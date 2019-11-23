using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int movementSpeed = 40;
    public float jumpForce = 5;
    //float gyroOffset = 0;

    public GameObject countdownTimerGO; 
    private TextMeshProUGUI countdownTimer;

    public float relativeRotation = 90;
    public bool movingAlongX = true;


    // These are used to determine when player has moved 50 blocks to generate 50 more
    public float xPosLastTrigger = 0;
    public float zPosLastTrigger = 0;




    void turnRight()
    {
        if (relativeRotation >= 270)
        {
            relativeRotation = 0;
        }
        else
        {
            relativeRotation += 90;
        }
        Debug.Log("Rotation" + relativeRotation);
        transform.Rotate(0, 90, 0, Space.World);
        movingAlongX = !movingAlongX;
        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;
    }

    void turnLeft()
    {
        if (relativeRotation <= 0)
        {
            relativeRotation = 270;
        }
        else
        {
            relativeRotation += -90;
        }
        Debug.Log("Rotation" + relativeRotation);
        transform.Rotate(0, -90, 0, Space.World);
        movingAlongX = !movingAlongX;
        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;
    }



    
    void Start()
    {
        countdownTimer = countdownTimerGO.GetComponent<TextMeshProUGUI>();

        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;
    }


    void Update()
    {
        if (Mathf.FloorToInt(Time.time) < 19)
        {
            countdownTimer.SetText("{0:0}", 19 - Mathf.FloorToInt(Time.time));
        } else
        {
            countdownTimer.SetText("GO!");
            if (Mathf.FloorToInt(Time.time) >= 21)
            {
                countdownTimerGO.SetActive(false);
            }
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }


        // This is for testing purpose so that we don't need a phone to activate swipe gestures
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            turnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            turnRight();
        }


        
        if (Mathf.Abs(xPosLastTrigger-transform.position.x) > 49)
        {
            GameObject.FindGameObjectWithTag("PathGenerator").GetComponent<PathGenerator>().generatePath();
            xPosLastTrigger = transform.position.x;
        }

        if (Mathf.Abs(zPosLastTrigger-transform.position.z) > 49)
        {
            GameObject.FindGameObjectWithTag("PathGenerator").GetComponent<PathGenerator>().generatePath();
            zPosLastTrigger = transform.position.z;
        }
    
    }
    

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        // Rotate 90 degrees when a swipe is detected
        if (data.Direction == SwipeDirection.Right)
        {
            turnRight();
        }
        else if (data.Direction == SwipeDirection.Left)
        {
            turnLeft();
        }
        else if (data.Direction == SwipeDirection.Up)
        {
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

}
