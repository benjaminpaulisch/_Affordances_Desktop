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
    

    // Start is called before the first frame update
    void Start()
    {
        m = Manager.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        // REAL CODE
        /*
        if (m.breakTime == true)
        {
            changingText.text = "Break time" + m.breakTimer;
        }
        else
        {
            changingText.text = m.DarkTimer + "not break time" + m.breakTimer;
        }
        
        if (m.CurrentTrial <= 2)
        {
            if (m.isDark == true)
            {
                if (m.breakTime == true)
                {
                    changingText.text = "Break Time!!!";
                }

                else
                {
                    changingText.text = "Please wait for the door to change colour.";
                }
            }

            if (m.isDark != true && m.doorRed != true && m.GoNoGoState == "Go")
            {
                changingText.text = "Please continue to the next room and touch the ring.";
            }

            if (m.doorRed == true)
            {
                changingText.text = "Please turn around and fill out the Questionnaire.";
            }

            if (m.circleTouched == true)
            {
                changingText.text = "Please go back to the starting point and fill out the Questionnaire.";
            }
        }

        if (m.CurrentTrial > 2 && m.CurrentTrial <= 10)
        {
            if (m.isDark == true)
            {
                if (m.breakTime == true)
                { changingText.text = "Break Time!"; }
                else
                {
                    changingText.text = "Wait for the door.";
                }
            }

            if (m.isDark != true && m.doorRed != true && m.GoNoGoState == "Go")
            {
                changingText.text = "Touch the ring.";
            }


            if (m.doorRed == true)
            {
                changingText.text = "Questionnaire.";
            }


            if (m.circleTouched == true)
            {
                changingText.text = "Questionnaire.";
            }
           
        }
        if (m.samAnswered == true)
        {
            if (m.CurrentTrial == 2)
            {
                changingText.text = "You may take a break now. Press Space to proceed to the next trial when you feel ready for it.";
            }
            else
                changingText.text = "Press Space to proceed to the next trial.";
        }
    }
        */




        //FOR TESTING
        if (m.shortBreak)
        {
            changingText.text = "You can now take a short break. Press the Space Bar when you are ready to proceed to the next Trial.";
        }

        if (m.breakTime == true)
        {
            changingText.text = "Mandatory break " + m.breakTimer.ToString("#") + "seconds";
        }
        if (m.breakTimeBlack && !m.breakTime && m.samAnswered)
        {
            changingText.text = "You can now start the next Trial by pressing the space bar.";
        }
        /*else
        {
            changingText.text = "blabla " + m.DarkTimer.ToString("#");
        }
        */
        
       
        if (m.CurrentTrial <= 2 && !m.breakTime && !m.breakTimeBlack && !m.shortBreak)
        {
            if (m.isDark == true)
            {
                changingText.text = "Please wait for the door to change colour.";
            }

            if (m.isDark != true && m.doorRed != true && m.GoNoGoState == "Go")
            {
                changingText.text = "Please continue to the next room and touch the ring.";
            }

            if (m.doorRed == true)
            {
                changingText.text = "Please turn around and fill out the Questionnaire.";
            }

            if (m.circleTouched == true || m.narrowDoorTouched == true)
            {
                changingText.text = "Please go back to the starting point and fill out the Questionnaire.";
            }
            if (m.samAnswered)
            {
                changingText.text = "Press the Space Bar to continue to the next Trial.";
            }
        }

        if (m.CurrentTrial > 2 && m.CurrentTrial <= 10 && !m.breakTime && !m.breakTimeBlack && !m.shortBreak)
        {
            if (m.isDark == true)
            {
                changingText.text = "Please wait for the door to change colour.";
            }

            if (m.isDark != true && m.doorRed != true && m.GoNoGoState == "Go")
            {
                changingText.text = "Please continue to the next room and touch the ring.";
            }

            if (m.doorRed == true)
            {
                changingText.text = "Please turn around and fill out the Questionnaire.";
            }

            if (m.circleTouched == true || m.narrowDoorTouched == true)
            {
                changingText.text = "Please go back to the starting point and fill out the Questionnaire.";
            }
            if (m.samAnswered)
            {
                changingText.text = "Press the Space Bar to continue to the next Trial.";
            }
        }
    }
}

    