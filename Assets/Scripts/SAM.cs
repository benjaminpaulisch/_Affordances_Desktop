using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.LSL4Unity.Scripts;

public class SAM : MonoBehaviour
{
    public GameObject marker;
    public LSLMarkerStream sM;
    public GameObject Manager;
    private Manager m;
    public GameObject startSquare;
    private StartSquare insideSquareScript;
    public bool insideSquare;
    public bool allAnswered = false;
    private bool canAnswer;
    public Color selectedColor;
    public GameObject selectionSquareArousal;
    public GameObject selectionSquareValence;
    public GameObject selectionSquareDominance;
    //Only Arousal
    public GameObject Arousal;
    public bool answeredArousal;
    private GameObject A0;
    private GameObject A1;
    private GameObject A2;
    private GameObject A3;
    private GameObject A4;
    //Only Valence
    public GameObject Valence;
    private GameObject V0;
    private GameObject V1;
    private GameObject V2;
    private GameObject V3;
    private GameObject V4;
    public bool answeredValence;
    //Only Dominance
    public GameObject Dominance;
    private GameObject D0;
    private GameObject D1;
    private GameObject D2;
    private GameObject D3;
    private GameObject D4;
    public bool answeredDominance;
    
    // Start is called before the first frame update
    void Start()
    {
        sM = marker.GetComponent<LSLMarkerStream>();
        m = Manager.GetComponent<Manager>();
        insideSquareScript = startSquare.GetComponent<StartSquare>();
        selectionSquareArousal.SetActive(false);
        selectionSquareValence.SetActive(false);
        selectionSquareDominance.SetActive(false);

        //Setting all SAM's off
        Arousal.SetActive(false);
        Valence.SetActive(false);
        Dominance.SetActive(false);

        //Get all SAM's Arousal
        A0 = Arousal.transform.GetChild(0).gameObject;
        A1 = Arousal.transform.GetChild(1).gameObject;
        A2 = Arousal.transform.GetChild(2).gameObject;
        A3 = Arousal.transform.GetChild(3).gameObject;
        A4 = Arousal.transform.GetChild(4).gameObject;

        //Get all SAM's Valence
        V0 = Valence.transform.GetChild(0).gameObject;
        V1 = Valence.transform.GetChild(1).gameObject;
        V2 = Valence.transform.GetChild(2).gameObject;
        V3 = Valence.transform.GetChild(3).gameObject;
        V4 = Valence.transform.GetChild(4).gameObject;

        //Get all SAM's Dominance
        D0 = Dominance.transform.GetChild(0).gameObject;
        D1 = Dominance.transform.GetChild(1).gameObject;
        D2 = Dominance.transform.GetChild(2).gameObject;
        D3 = Dominance.transform.GetChild(3).gameObject;
        D4 = Dominance.transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (m.isDark) {
            allAnswered = false;
            answeredArousal = false;
            answeredValence = false;
            answeredDominance = false;
            selectionSquareArousal.SetActive(false);
            selectionSquareValence.SetActive(false);
            selectionSquareDominance.SetActive(false);
        }

        if (insideSquareScript.insideSquare && !m.isDark && (m.circleTouched || m.doorRed || m.narrowDoorTouched)) {
            canAnswer = true;
        }
       /*if (insideSquareScript.insideSquare && !m.isDark && m.doorRed)
        {
            canAnswer = true;
        }*/
        else {
            canAnswer = false;
        }

        //The first questionnaire--Arousal
        if (canAnswer && !answeredArousal && !answeredValence && !answeredDominance) {
            //Setting Arousal on first
            Arousal.SetActive(true);

            if (Input.GetKeyDown("1")){
                selectionSquareArousal.SetActive(true);
                if (Input.GetKeyDown("1") && selectionSquareArousal.transform.position == A0.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Arousal1");
                    m.eventMarkerRun = false;
                    sendMarker(";Arousal1");
                    StartCoroutine(Selected(A0));
                    StartCoroutine(Selected(selectionSquareArousal));
                    StartCoroutine(RemoveQuestionnaire(Arousal));
                }
                selectionSquareArousal.transform.position = A0.transform.position;
            }
            if (Input.GetKeyDown("2")){
                selectionSquareArousal.SetActive(true);
                if (Input.GetKeyDown("2") && selectionSquareArousal.transform.position == A1.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Arousal2");
                    m.eventMarkerRun = false;
                    sendMarker(";Arousal2");
                    StartCoroutine(Selected(A1));
                    StartCoroutine(Selected(selectionSquareArousal));
                    StartCoroutine(RemoveQuestionnaire(Arousal));
                }
                selectionSquareArousal.transform.position = A1.transform.position;
            }
            if (Input.GetKeyDown("3")){
                selectionSquareArousal.SetActive(true);
                if (Input.GetKeyDown("3") && selectionSquareArousal.transform.position == A2.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Arousal3");
                    m.eventMarkerRun = false;
                    sendMarker(";Arousal3");
                    StartCoroutine(Selected(A2));
                    StartCoroutine(Selected(selectionSquareArousal));
                    StartCoroutine(RemoveQuestionnaire(Arousal));
                }
                selectionSquareArousal.transform.position = A2.transform.position;
            }
            if (Input.GetKeyDown("4")){
                selectionSquareArousal.SetActive(true);
                if (Input.GetKeyDown("4") && selectionSquareArousal.transform.position == A3.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Arousal4");
                    m.eventMarkerRun = false;
                    sendMarker(";Arousal4");
                    StartCoroutine(Selected(A3));
                    StartCoroutine(Selected(selectionSquareArousal));
                    StartCoroutine(RemoveQuestionnaire(Arousal));
                }
                selectionSquareArousal.transform.position = A3.transform.position;
            }
            if (Input.GetKeyDown("5")){
                selectionSquareArousal.SetActive(true);
                if (Input.GetKeyDown("5") && selectionSquareArousal.transform.position == A4.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Arousal5");
                    m.eventMarkerRun = false;
                    sendMarker(";Arousal5");
                    StartCoroutine(Selected(A4));
                    StartCoroutine(Selected(selectionSquareArousal));
                    StartCoroutine(RemoveQuestionnaire(Arousal));
                }
                selectionSquareArousal.transform.position = A4.transform.position;
            }
        }

        //The second questionnaire--Valence
        if (canAnswer && answeredArousal && !answeredValence && !answeredDominance) {
            //Setting Valence on first
            Valence.SetActive(true);

            if (Input.GetKeyDown("1")){
                selectionSquareValence.SetActive(true);
                if (Input.GetKeyDown("1") && selectionSquareValence.transform.position == V0.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Valence1");
                    m.eventMarkerRun = false;
                    sendMarker(";Valence1");
                    StartCoroutine(Selected(V0));
                    StartCoroutine(Selected(selectionSquareValence));
                    StartCoroutine(RemoveQuestionnaire(Valence));
                }
                selectionSquareValence.transform.position = V0.transform.position;
            }
            if (Input.GetKeyDown("2")){
                selectionSquareValence.SetActive(true);
                if (Input.GetKeyDown("2") && selectionSquareValence.transform.position == V1.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Valence2");
                    m.eventMarkerRun = false;
                    sendMarker(";Valence2");
                    StartCoroutine(Selected(V1));
                    StartCoroutine(Selected(selectionSquareValence));
                    StartCoroutine(RemoveQuestionnaire(Valence));
                }
                selectionSquareValence.transform.position = V1.transform.position;
            }
            if (Input.GetKeyDown("3")){
                selectionSquareValence.SetActive(true);
                if (Input.GetKeyDown("3") && selectionSquareValence.transform.position == V2.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Valence3");
                    m.eventMarkerRun = false;
                    sendMarker(";Valence3");
                    StartCoroutine(Selected(V2));
                    StartCoroutine(Selected(selectionSquareValence));
                    StartCoroutine(RemoveQuestionnaire(Valence));
                }
                selectionSquareValence.transform.position = V2.transform.position;
            }
            if (Input.GetKeyDown("4")){
                selectionSquareValence.SetActive(true);
                if (Input.GetKeyDown("4") && selectionSquareValence.transform.position == V3.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Valence4");
                    m.eventMarkerRun = false;
                    sendMarker(";Valence4");
                    StartCoroutine(Selected(V3));
                    StartCoroutine(Selected(selectionSquareValence));
                    StartCoroutine(RemoveQuestionnaire(Valence));
                }
                selectionSquareValence.transform.position = V3.transform.position;
            }
            if (Input.GetKeyDown("5")){
                selectionSquareValence.SetActive(true);
                if (Input.GetKeyDown("5") && selectionSquareValence.transform.position == V4.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Valence5");
                    m.eventMarkerRun = false;
                    sendMarker(";Valence5");
                    StartCoroutine(Selected(V4));
                    StartCoroutine(Selected(selectionSquareValence));
                    StartCoroutine(RemoveQuestionnaire(Valence));
                }
                selectionSquareValence.transform.position = V4.transform.position;
            }
        }

        //The third questionnaire--Dominance
        if (canAnswer && answeredArousal && answeredValence && !answeredDominance) {
            //Setting Dominance on 
            Dominance.SetActive(true);

            if (Input.GetKeyDown("1")){
                selectionSquareDominance.SetActive(true);
                if (Input.GetKeyDown("1") && selectionSquareDominance.transform.position == D0.transform.position){
                    //Debug.Log(m.CurrentStatus + ";Dominance1");
                    m.eventMarkerRun = false;
                    sendMarker(";Dominance1");
                    StartCoroutine(Selected(D0));
                    StartCoroutine(Selected(selectionSquareDominance));
                    StartCoroutine(RemoveQuestionnaire(Dominance));
                }
                selectionSquareDominance.transform.position = D0.transform.position;
            }
            if (Input.GetKeyDown("2")){
                selectionSquareDominance.SetActive(true);
                if (Input.GetKeyDown("2") && selectionSquareDominance.transform.position == D1.transform.position){
                    //Debug.Log("#" + m.CurrentTrial.ToString() + "Dominance2");
                    m.eventMarkerRun = false;
                    sendMarker(";Dominance2");
                    StartCoroutine(Selected(D1));
                    StartCoroutine(Selected(selectionSquareDominance));
                    StartCoroutine(RemoveQuestionnaire(Dominance));
                }
                selectionSquareDominance.transform.position = D1.transform.position;
            }
            if (Input.GetKeyDown("3")){
                selectionSquareDominance.SetActive(true);
                if (Input.GetKeyDown("3") && selectionSquareDominance.transform.position == D2.transform.position){
                    //Debug.Log("#" + m.CurrentTrial.ToString() + "Dominance3");
                    m.eventMarkerRun = false;
                    sendMarker(";Dominance3");
                    StartCoroutine(Selected(D2));
                    StartCoroutine(Selected(selectionSquareDominance));
                    StartCoroutine(RemoveQuestionnaire(Dominance));
                }
                selectionSquareDominance.transform.position = D2.transform.position;
            }
            if (Input.GetKeyDown("4")){
                selectionSquareDominance.SetActive(true);
                if (Input.GetKeyDown("4") && selectionSquareDominance.transform.position == D3.transform.position){
                    //Debug.Log("#" + m.CurrentTrial.ToString() + "Dominance4");
                    m.eventMarkerRun = false;
                    sendMarker(";Dominance4");
                    StartCoroutine(Selected(D3));
                    StartCoroutine(Selected(selectionSquareDominance));
                    StartCoroutine(RemoveQuestionnaire(Dominance));
                }
                selectionSquareDominance.transform.position = D3.transform.position;
            }
            if (Input.GetKeyDown("5")){
                selectionSquareDominance.SetActive(true);
                if (Input.GetKeyDown("5") && selectionSquareDominance.transform.position == D4.transform.position){
                    //Debug.Log("#" + m.CurrentTrial.ToString() + "Dominance5");
                    m.eventMarkerRun = false;
                    sendMarker(";Dominance5");
                    StartCoroutine(Selected(D4));
                    StartCoroutine(Selected(selectionSquareDominance));
                    StartCoroutine(RemoveQuestionnaire(Dominance));
                }
                selectionSquareDominance.transform.position = D4.transform.position;
            }
        }

        //Resetting the color of the selection square for next trial
        if (!canAnswer || (answeredArousal && answeredDominance && answeredValence)){
            StartCoroutine(ResettingColor(selectionSquareArousal, A0, A1, A2, A3, A4));
            StartCoroutine(ResettingColor(selectionSquareValence, V0, V1, V2, V3, V4));
            StartCoroutine(ResettingColor(selectionSquareDominance, D0, D1, D2, D3, D4));
        }

        if (answeredArousal && answeredDominance && answeredValence) {
            allAnswered = true;
        } else {
            allAnswered = false;
        }

        //Fading the color of the square
        IEnumerator Selected(GameObject SAM){
            for (float f = 1f;f > -0.05;f -= 0.01f) 
              {
                  selectedColor.a = f;
                  SAM.GetComponent<SpriteRenderer>().color = selectedColor;
                  yield return new WaitForSeconds(0.01f);
              }
        }

        //Resetting the color for next trials
        IEnumerator ResettingColor(GameObject sq, GameObject one, GameObject two, GameObject three, GameObject four, GameObject five) {
            sq.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            one.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            two.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            three.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            four.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            five.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
            yield return null;
        }

        //Removing a questionnaire
        IEnumerator RemoveQuestionnaire(GameObject q) {
            yield return new WaitForSeconds(1f);
            if (q.name == "SAM_Arousal") {answeredArousal = true;}
            if (q.name == "SAM_Valence") {answeredValence = true;}
            if (q.name == "SAM_Dominance") {answeredDominance = true;}
            q.SetActive(false);
        }

        void sendMarker(string SAM)
    {
    if (!m.eventMarkerRun)
        {
            print(m.CurrentStatus + SAM);
            sM.Write(m.CurrentStatus + SAM);
            m.eventMarkerRun = true;
        }
    }
    }
}
