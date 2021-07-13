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
    [Header("Experiment settings")]
    public int TotalNumberTrials = 240;
    public int TimeInDark = 5;
    public int DarkSD = 2;
    public List<int> TimeInDarkList;
    public int TimeToImperative = 4;
    public int LightSD = 2;
    public List<int> TimeToImperativeList;

    [Header("Object references")]
    public GameObject Sphere;
    public GameObject gameobjectSAM;
    private SAM SAMcode;
    public LSLMarkerStream marker;
    public GameObject canvasMenu;

    //"Timer and status"
    [HideInInspector] public string CurrentStatus;
    private string LightStatus;
    private float DarkTimer = 0f;
    private float LightTimer = 0f;
    [HideInInspector] public bool isDark;
    [HideInInspector] public bool shownImperative;
    [HideInInspector] public bool circleTouched = false;

    // this part was added by Christoph
    [HideInInspector] public bool doorRed = false;
    [HideInInspector] public bool samAnswered = false;
    [HideInInspector] public bool breakTime = false;
    [HideInInspector] public float breakTimer = 0f;
    private bool breakTimeSet = false;
    [HideInInspector] public bool narrowDoorTouched = false;
    [HideInInspector] public bool breakTimeBlack = false;
    [HideInInspector] public bool shortBreak = false;
    // end of Christoph's part

    //"Managing trials"
    [HideInInspector] public int CurrentTrial = 0;
    //public bool trialDone = false;
    [HideInInspector] public string CurrentDoorType;
    [HideInInspector] public string GoNoGoState;
    public List<string> GoNoGoList;
    public List<int> DoorCaseList;
    private bool runOnceLight;

    //"LSL settings"
    [HideInInspector] public bool eventMarkerRun = false;
    private bool LightsOnRun = false;
    private bool LightsOffRun = false;
    private bool GoCueRun = false;

    //programm control and status
    private int status;



    // Start is called before the first frame update
    void Start()
    {
        
        //Getting LSL stream
        //marker = FindObjectOfType<LSLMarkerStream>();

        /*
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

        //Generating randomized list of time to imperative
        for (int i = 0; i < TotalNumberTrials; i++)
        {
            TimeToImperativeList.Add(rnd.Next(TimeToImperative-LightSD, TimeToImperative+LightSD));
        }

        //Removing the cursor in-game
        Cursor.visible = false;

        //Setting start status
        isDark = true;
        DarkTimer = 5f;     //[BPA:] why is this set to a hard coded value when later it get's a different value each trial (except the first)?
        //if (CurrentTrial == 1) { DarkTimer = 10f; }
        LightTimer = TimeToImperativeList[CurrentTrial];
        GoNoGoState = "";
        samAnswered = false;
        */

        //Retrieving the SAM questionnaire
        SAMcode = gameobjectSAM.GetComponent<SAM>();


        //ToDo: run Menu
        startMenu();

    }


    void startMenu()
    {
        Console.Write("Starting main menu");

        //set program status to 0
        status = 0;

        //activate sphere to have a "black screen"
        Sphere.SetActive(true);

        //activate menu
        canvasMenu.SetActive(true);

    }


    public void startTraining()
    {
        Console.Write("Starting training");

        //set program status to 1
        status = 1;

        //deactivate menu
        canvasMenu.SetActive(false);

        //initTraining


        //Removing the cursor in-game
        Cursor.visible = false;

    }


    public void startExperiment()
    {
        Console.Write("Starting experiment");

        //set program status to 2
        status = 2;

        //deactivate menu
        canvasMenu.SetActive(false);

        //initialize experiment
        initExperiment();


        //Removing the cursor in-game
        Cursor.visible = false;

    }


    void initTraining()
    {
        Console.Write("Initializing training");


    }


    void initExperiment()
    {
        Console.Write("Initializing experiment");

        //Generating pseudo-randomized trial list
        for (int i = 0; i < (TotalNumberTrials / 3); i++)
        {
            DoorCaseList.Add(1);
            DoorCaseList.Add(2);
            DoorCaseList.Add(3);
            DoorCaseList = DoorCaseList.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        //Generating pseudo-randomized GoNoGo list
        for (int i = 0; i < (TotalNumberTrials / 2); i++)
        {
            GoNoGoList.Add("Go");
            GoNoGoList.Add("NoGo");
            GoNoGoList = GoNoGoList.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        //Generating randomized list of time in dark
        System.Random rnd = new System.Random();
        for (int i = 0; i < TotalNumberTrials; i++)
        {
            TimeInDarkList.Add(rnd.Next(TimeInDark - DarkSD, TimeInDark + DarkSD));
        }

        //Generating randomized list of time to imperative
        for (int i = 0; i < TotalNumberTrials; i++)
        {
            TimeToImperativeList.Add(rnd.Next(TimeToImperative - LightSD, TimeToImperative + LightSD));
        }


        //Setting start status
        isDark = true;
        DarkTimer = 5f;     //[BPA:] why is this set to a hard coded value when later it get's a different value each trial (except the first)?
        //if (CurrentTrial == 1) { DarkTimer = 10f; }
        LightTimer = TimeToImperativeList[CurrentTrial];
        GoNoGoState = "";
        samAnswered = false;

    }

    void runExperiment()
    {
        //Setting current status
        //[BPA]:better use a switch case here:
        if (DoorCaseList[CurrentTrial] == 1) { CurrentDoorType = "Narrow"; }
        if (DoorCaseList[CurrentTrial] == 2) { CurrentDoorType = "Mid"; }
        if (DoorCaseList[CurrentTrial] == 3) { CurrentDoorType = "Wide"; }

        //[BPA]:better use a switch case here:
        if (isDark == false) { LightStatus = "LightsOn"; }
        if (isDark == true) { LightStatus = "Dark"; }
        if (isDark == true) { GoNoGoState = ""; }

        CurrentStatus = "#" + CurrentTrial.ToString() + ";" + CurrentDoorType + ";" + LightStatus + ";" + GoNoGoState;
        //if (CurrentTrial == 1) { DarkTimer = 10f; }



        //Sending the LSL-markers when:

        // Lights on:
        if (DarkTimer <= 0 && !isDark)
        {
            if (!LightsOnRun)
            {
                eventMarkerRun = false;
                sendMarker();
                LightsOnRun = true;
                breakTime = false;
                //circleTouched = false;
            }
        }
        // Lights off:
        if (DarkTimer >= 0 && isDark)
        {
            if (!LightsOffRun)
            {
                eventMarkerRun = false;
                sendMarker();
                LightsOffRun = true;
                //circleTouched = false;
            }
        }
        // Go/NoGo cue:
        if (shownImperative)
        {
            if (!GoCueRun)
            {
                eventMarkerRun = false;
                sendMarker();
                GoCueRun = true;
                //circleTouched = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //The Paradigm:

        //Countdown for dark
        if (DarkTimer >= 0 && isDark)
        {
            DarkTimer -= Time.deltaTime;
            Sphere.SetActive(true);
        }

        //Changes because it's now Light
        if (DarkTimer <= 0 && !breakTimeBlack)
        {
            isDark = false;
            Sphere.SetActive(false);
        }

        //Countdown for imperative
        if (LightTimer >= 0 && !isDark)
        {
            LightTimer -= Time.deltaTime;
        }

        //Changes because of imperative stimulus
        if (LightTimer <= 0 && !runOnceLight)
        {
            GoNoGoState = GoNoGoList[CurrentTrial];
            if (GoNoGoState == "NoGo")
            {
                doorRed = true;
            }
            shownImperative = true;
            runOnceLight = true;
        }
        if (SAMcode.allAnswered)
        {
            samAnswered = true;
        }


        // FOR TESTING 
        if ((CurrentTrial == 5 || CurrentTrial == 10) && samAnswered)
        {
            breakTime = true;
        }

        if (SAMcode.allAnswered && breakTime && !breakTimeSet)
        {
            breakTimer = 10f;
            breakTimeSet = true;
        }
        //Still uncertain which times should be chosen for optional short breaks
        if ((CurrentTrial == 3 || CurrentTrial == 7) && samAnswered)
        {
            shortBreak = true;
        }


        //REAL CODE
        /*if ((CurrentTrial == 90 || CurrentTrial == 180) && samAnswered)
        {
            breakTime = true;
        }

        if (SAMcode.allAnswered && breakTime && !breakTimeSet)
        {
            breakTimer = 600f;
            breakTimeSet = true;
        }
        */

        if (breakTimer > 0 && SAMcode.allAnswered)
        {
            breakTimer -= Time.deltaTime;
            Sphere.SetActive(true);
            breakTimeBlack = true;
        }

        if (breakTimer <= 0)
        {
            breakTime = false;
        }

        //Once the trial is done
        if (Input.GetKeyDown(KeyCode.Space) && SAMcode.allAnswered && !breakTime)
        {
            CurrentTrial += 1;
            DarkTimer = TimeInDarkList[CurrentTrial];
            /*if (CurrentTrial == 1 || CurrentTrial == 4)
            { DarkTimer = 1f;
                breakTime = true;
            }
            */
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
            breakTimeSet = false;
            narrowDoorTouched = false;
            breakTimeBlack = false;
            shortBreak = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Setting current status
        //[BPA]:better use a switch case here:
        if (DoorCaseList[CurrentTrial] == 1) {CurrentDoorType = "Narrow";}
        if (DoorCaseList[CurrentTrial] == 2) {CurrentDoorType = "Mid";}
        if (DoorCaseList[CurrentTrial] == 3) {CurrentDoorType = "Wide";}

        //[BPA]:better use a switch case here:
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
                breakTime = false;
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
        if (DarkTimer <= 0 && !breakTimeBlack) {
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


        // FOR TESTING 
        if ((CurrentTrial == 5 || CurrentTrial == 10) && samAnswered)
        {
            breakTime = true;
        }

        if (SAMcode.allAnswered && breakTime && !breakTimeSet)
        {
            breakTimer = 10f;
            breakTimeSet = true;
        }
        //Still uncertain which times should be chosen for optional short breaks
        if ((CurrentTrial == 3 || CurrentTrial == 7) && samAnswered)
        {
            shortBreak = true;
        }

        //REAL CODE
        /if ((CurrentTrial == 90 || CurrentTrial == 180) && samAnswered)
        {
            breakTime = true;
        }

        if (SAMcode.allAnswered && breakTime && !breakTimeSet)
        {
            breakTimer = 600f;
            breakTimeSet = true;
        }
        /
        

        if (breakTimer > 0 && SAMcode.allAnswered)
        {
            breakTimer -= Time.deltaTime;
            Sphere.SetActive(true);
            breakTimeBlack = true;
        }

        if (breakTimer <= 0)
        {
            breakTime = false;
        }

        //Once the trial is done
        if (Input.GetKeyDown(KeyCode.Space) && SAMcode.allAnswered && !breakTime){
            CurrentTrial += 1;
            DarkTimer = TimeInDarkList[CurrentTrial];
            /if (CurrentTrial == 1 || CurrentTrial == 4)
            { DarkTimer = 1f;
                breakTime = true;
            }
            /
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
            breakTimeSet = false;
            narrowDoorTouched = false;
            breakTimeBlack = false;
            shortBreak = false;
        }
        */


        //status loop:
        switch(status)
        {
            case 0: //menu
                {
                    break;
                }
            case 1: //training
                {
                    //run training


                    break;
                }
            case 2: //experiment
                {
                    //run experiment
                    runExperiment();

                    break;
                }

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
