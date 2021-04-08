using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using System.Linq;
using Assets.LSL4Unity.Scripts;
using System.IO;
using UnityEngine.XR;
using System;
using LSL;


public class Manager : MonoBehaviour
{
    [Header("Timer and status")]
    public string CurrentStatus;
    public string LightStatus;
    public float DarkTimer = 0f;
    public float LightTimer = 0f;
    public bool isDark;
    public bool shownImperative;
    public bool circleTouched = false;
    public bool doorRed = false;
    public bool samAnswered = false;

    [Header("Managing trials")]
    public int TotalNumberTrials = 240;
    public int CurrentTrial = 0;
    //public bool trialDone = false;
    public string CurrentDoorType;
    public string GoNoGoState;
    public List<string> GoNoGoList;
    public List<int> DoorCaseList;
    private bool runOnceLight;

    [Header("Questionnaire")]
    public GameObject gameobjectSAM;
    private SAM SAMcode;
    [Header ("LSL settings")]
    public LSLMarkerStream marker;
    public bool eventMarkerRun = false;
    public bool LightsOnRun = false;
    public bool LightsOffRun = false;
    public bool GoCueRun = false;


    [Header("Game settings")]
    public int TimeInDark = 5;
    public int DarkSD = 2;
    public List<int> TimeInDarkList;
    public int TimeToImperative = 4;
    public int LightSD = 2;
    public List<int> TimeToImperativeList;
    public GameObject Sphere;
    //public Text myText;


    // Start is called before the first frame update
    void Start()
    {
        
        //Getting LSL stream
        marker = FindObjectOfType<LSLMarkerStream>();

        //Generating pseudo-randomized trial list
        for (int i = 0; i < (TotalNumberTrials / 3); i++)
        {
            DoorCaseList.Add(1);
            DoorCaseList.Add(2);
            DoorCaseList.Add(3);
            DoorCaseList = DoorCaseList.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        //Generating pseudo-randomized GoNoGo list
        for (int i = 0; i < (TotalNumberTrials/2); i++)
        {
            GoNoGoList.Add("Go");
            GoNoGoList.Add("NoGo");
            GoNoGoList = GoNoGoList.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        //Generating randomized list of time in dark
        System.Random rnd = new System.Random();
        for (int i = 0; i < TotalNumberTrials; i++)
        {
            TimeInDarkList.Add(rnd.Next(TimeInDark-DarkSD, TimeInDark+DarkSD));
        }

        //Generating randomized list of time in dark
        for (int i = 0; i < TotalNumberTrials; i++)
        {
            TimeToImperativeList.Add(rnd.Next(TimeToImperative-LightSD, TimeToImperative+LightSD));
        }

        //Removing the cursor in-game
        Cursor.visible = false;

        //Setting start status
        isDark = true;
        DarkTimer = 5f;
        //if (CurrentTrial == 1) { DarkTimer = 10f; }
        LightTimer = TimeToImperativeList[CurrentTrial];
        GoNoGoState = "";
        samAnswered = false;

        //Retrieving the SAM questionnaire
        SAMcode = gameobjectSAM.GetComponent<SAM>();

       
    }

    // Update is called once per frame
    void Update()
    {

        //Setting current status
        if (DoorCaseList[CurrentTrial] == 1) {CurrentDoorType = "Narrow";}
        if (DoorCaseList[CurrentTrial] == 2) {CurrentDoorType = "Mid";}
        if (DoorCaseList[CurrentTrial] == 3) {CurrentDoorType = "Wide";}
        if (isDark == false) {LightStatus = "LightsOn";}
        if (isDark == true) {LightStatus = "Dark";}
        if (isDark == true) {GoNoGoState = "";}
        CurrentStatus = "#" + CurrentTrial.ToString() + ";" + CurrentDoorType + ";" + LightStatus + ";" + GoNoGoState;
        //if (CurrentTrial == 1) { DarkTimer = 10f; }



        //Sending the LSL-markers when:

        // Lights on:
        if (DarkTimer <= 0 && !isDark) {
                if (!LightsOnRun) {
                eventMarkerRun = false;
                sendMarker();
                LightsOnRun = true;
                //circleTouched = false;
            }
        }
        // Lights off:
        if (DarkTimer >= 0 && isDark) {
                if (!LightsOffRun) {
                eventMarkerRun = false;
                sendMarker();
                LightsOffRun = true;
                //circleTouched = false;
            }
        }
        // Go/NoGo cue:
        if (shownImperative) {
                if (!GoCueRun) {
                eventMarkerRun = false;
                sendMarker();
                GoCueRun = true;
                //circleTouched = false;
            }
        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //The Paradigm:

        //Countdown for dark
        if (DarkTimer >= 0 && isDark) {
            DarkTimer -= Time.deltaTime;
            Sphere.SetActive(true);
        }

        //Changes because it's now Light
        if (DarkTimer <= 0) {
            isDark = false;
            Sphere.SetActive(false);
        }

        //Countdown for imperative
        if (LightTimer >= 0 && !isDark) {
            LightTimer -= Time.deltaTime;
        }

        //Changes because of imperative stimulus
        if (LightTimer <= 0 && !runOnceLight) {
            GoNoGoState = GoNoGoList[CurrentTrial];
            if (GoNoGoState == "NoGo") {
                doorRed = true;
            }
            shownImperative = true;
            runOnceLight = true;
        }
        if (SAMcode.allAnswered)
        {
            samAnswered = true;
        }

        //Once the trial is done
        if (Input.GetKeyDown(KeyCode.Space) && SAMcode.allAnswered){
            CurrentTrial += 1;
            DarkTimer = TimeInDarkList[CurrentTrial];
            if (CurrentTrial == 1)
            { DarkTimer = 10f; }
            //DarkTimer = TimeInDarkList[CurrentTrial];
            LightTimer = TimeToImperativeList[CurrentTrial];
            runOnceLight = false;
            shownImperative = false;
            isDark = true;
            circleTouched = false;
            LightsOnRun = false;
            LightsOffRun = false;
            GoCueRun = false;
            doorRed = false;
            samAnswered = false;
        }
    }

    //Sending marker
    void sendMarker()
    {
        if (!eventMarkerRun)
        {
            print(CurrentStatus);
            marker.Write(CurrentStatus);
            eventMarkerRun = true;
        }
        //eventMarkerRun = true;
    }
}
