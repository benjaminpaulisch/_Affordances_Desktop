using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.LSL4Unity.Scripts;
using System.IO;
using UnityEngine.XR;
using System;
using LSL;

public class ChangeText : MonoBehaviour
{
    public Text changingText;
    public GameObject Manager;
    private Manager m;
    /*public GameObject gameobjectSAM;
    private SAM SAMcode;
    */

    // Start is called before the first frame update
    void Start()
    {
        //changingText.text = "hallo";
        m = Manager.GetComponent<Manager>();
        //changingText.text = "hallo";
    }

    // Update is called once per frame
    void Update()
    {
        if (m.isDark == true)
        {
            changingText.text = "Please wait for the door to change colour.";
        }
        /*if (m.isDark != true)
        {
            changingText.text = "Wait for the door to change colour";
        }
        */
        if (m.isDark != true && m.doorRed != true && m.GoNoGoState == "Go")
        {
            changingText.text = "Please continue to the next room and touch the ring.";
        }
        /*if (m.isDark != true && m.doorRed != true && m.CurrentTrial == 2)
        {
            changingText.text = "You may take a break now. If you want to continue, please proceed to the next room and touch the ring.";
        }
        */
       
        if (m.doorRed == true)
        {
            changingText.text = "Please turn around and fill out the Questionnaire.";
        }

        /*if (m.doorRed == true && m.CurrentTrial == 2)
        {
            changingText.text = "You may take a break now. If you want to continue, please turn around and fill out the Questionnaire.";
        }
        */
        if (m.circleTouched == true)
        {
            changingText.text = "Please go back to the starting point and fill out the Questionnaire.";
        }
        if (m.samAnswered == true)
        {
            if (m.CurrentTrial == 30)
            {
            changingText.text = "You may take a break now. Press Space to proceed to the next trial when you feel ready for it.";
            }
            else
            changingText.text = "Press Space to proceed to the next trial.";
        }
    }

    /*public void TextChange()
    {
        changingText.text = "2";
    }*/
}
