using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorCollision : MonoBehaviour
{
    public GameObject Manager;
    public GameObject Door;
    private Manager m;
    public Vector3 zeroPosition;
    public Color originalColor;
    public float time = 0f;
    public Color NoGoColor;
    public Color GoColor;
    public bool doneChange;
    //private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m = Manager.GetComponent<Manager>();
        zeroPosition = Door.transform.position;

        originalColor = Door.GetComponent<Renderer>().material.color;
        doneChange = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (m.shownImperative && !m.isDark){
            ColourChanging(m.GoNoGoState);
        }

        if (m.isDark) {
            Door.GetComponent<Renderer>().material.color = originalColor;
            Door.transform.position = zeroPosition;
            time = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (m.shownImperative){
            StartCoroutine(SlideDoor());
            if (m.GoNoGoState == "Go" && m.CurrentDoorType == "Narrow") {
                m.narrowDoorTouched = true;
            }
        }
    }

    void ColourChanging(string GoNoGo)
    {
        if (GoNoGo == "NoGo" && 1 > time){
            Door.GetComponent<Renderer>().material.color = Color.Lerp(NoGoColor, originalColor, time += Time.deltaTime/2);
        }
        if (GoNoGo == "Go" && 1 > time){
            Door.GetComponent<Renderer>().material.color = Color.Lerp(GoColor, originalColor, time += Time.deltaTime/2);
        }
        if (time <= 0) {doneChange = true;}
    }

    IEnumerator SlideDoor(){
        for (float f = 0; f < 1; f += 0.005f)
        {
            Door.transform.position = Vector3.Lerp(Door.transform.position, new Vector3(2f, 0, 0), f);
            yield return new WaitForSeconds(.005f);
        }
    }
}
