using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAround : MonoBehaviour
{

    //private float MovementX;
    private float MovementY;
    private float Turning;
    [SerializeField] [Range(0f, 10f)] public float MovingSpeed;
    [SerializeField] [Range(0f, 120f)] public float TurningSpeed;
    public float PlayerHeight = 1.8f;

    // private float MouseX = 0.0f;
    // private float MouseY = 0.0f;

    // Start is called before the first frame update


    private Vector3 startPosition;




    void Start()
    {
        transform.position = new Vector3(-2.5f, PlayerHeight, 0f);
        Turning = 180f;
        TurningSpeed = 100f;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //MovementX = Input.GetAxis ("Horizontal");
        MovementY = Input.GetAxis("Vertical");

        if (MovementY != 0)
        {
            //transform.position += transform.right * MovementX * Time.deltaTime * MovingSpeed;
            transform.position += transform.forward * MovementY * Time.deltaTime * MovingSpeed;
            transform.position = new Vector3(transform.position.x, PlayerHeight, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Turning += Time.deltaTime * TurningSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Turning -= Time.deltaTime * TurningSpeed;
        }
        transform.eulerAngles = new Vector3(0.0f, Turning, 0.0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            {
                transform.position = startPosition;
                Turning = 180f;
            }

            // if (Input.GetMouseButton(0)){
            //     MouseX += MouseSpeed * Input.GetAxis("Mouse X");
            //     MouseY += MouseSpeed * Input.GetAxis("Mouse Y");
            //     transform.eulerAngles = new Vector3(MouseY, MouseX, 0.0f);
            // }

        }

    }
}
