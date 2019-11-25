using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int movementSpeed = 40;
    public float jumpForce = 5;

    public bool isMoving = false;

    public GameObject countdownTimerGO; 
    TextMeshProUGUI countdownTimer;

    public GameObject scoreTextGO;
    TextMeshProUGUI scoreText;

    public float relativeRotation = 90;

    public int score = 0;
    public bool isDead = false;


    // These are used to determine when player has moved 50 blocks to generate 50 more
    float xPosLastTrigger = 0;
    float zPosLastTrigger = 0;

    public float windowForRotation = 1;
    bool movingAlongX = true;



    public GameObject FindClosestPathBlock()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("PathBlockTurn");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }


    public bool CenterPlayerAndReturnIfPossible()
    {
        // Generate 50 path block to be safe ...
        GameObject.FindGameObjectWithTag("PathGenerator").GetComponent<PathGenerator>().generatePath();

        // Calculate distance for both x and z
        bool closeX = Mathf.Abs(FindClosestPathBlock().transform.position.x - transform.position.x) < windowForRotation;
        bool closeZ = Mathf.Abs(FindClosestPathBlock().transform.position.z - transform.position.z) < windowForRotation;

        // Check for x distance is movingAlongX else check for z
        if (movingAlongX && closeX)
        {
            transform.position = new Vector3(FindClosestPathBlock().transform.position.x, .5f, FindClosestPathBlock().transform.position.z);
            return true;
        }
        else if (!movingAlongX && closeZ)
        {
            transform.position = new Vector3(FindClosestPathBlock().transform.position.x, .5f, FindClosestPathBlock().transform.position.z);
            return true;
        }
        else
        {
            // Die
            isDead = true;
            isMoving = false;
            countdownTimer.SetText("Dead");
            countdownTimerGO.SetActive(true);
            return false;
        }
    }



    void TurnRight()
    {
        if (relativeRotation >= 270)
        {
            relativeRotation = 0;
        }
        else
        {
            relativeRotation += 90;
        }
        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;

        if (CenterPlayerAndReturnIfPossible())
        {
            transform.Rotate(0, 90, 0, Space.World);
        }

        movingAlongX = !movingAlongX;
    }

    void TurnLeft()
    {
        if (relativeRotation <= 0)
        {
            relativeRotation = 270;
        }
        else
        {
            relativeRotation += -90;
        }
        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;

        if (CenterPlayerAndReturnIfPossible())
        {
            transform.Rotate(0, -90, 0, Space.World);
        }

        movingAlongX = !movingAlongX;
    }



    
    void Start()
    {
        countdownTimer = countdownTimerGO.GetComponent<TextMeshProUGUI>();
        scoreText = scoreTextGO.GetComponent<TextMeshProUGUI>();

        xPosLastTrigger = transform.position.x;
        zPosLastTrigger = transform.position.z;
    }


    void Update()
    {
        if (Mathf.FloorToInt(Time.timeSinceLevelLoad) < 19)
        {
            countdownTimer.SetText("{0:0}", 19 - Mathf.FloorToInt(Time.timeSinceLevelLoad));
            scoreTextGO.SetActive(false);
        } else if (Mathf.FloorToInt(Time.timeSinceLevelLoad) >= 18 && Mathf.FloorToInt(Time.timeSinceLevelLoad) < 21)
        {
            countdownTimer.SetText("GO!");
            isMoving = true;
        } else
        {
            scoreTextGO.SetActive(true);
            if (Mathf.FloorToInt(Time.timeSinceLevelLoad) == 21)
            {
                countdownTimerGO.SetActive(false);
            }


            // Move player every frame after countdown
            if (!isDead)
            {
                transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            }
        }



        if (isMoving)
        {
            // Calculate score based on how far the player has traveled and set score text. If score is below 0 (which it is in countdown, then set to 0)
            score = Mathf.FloorToInt(movementSpeed * (Time.timeSinceLevelLoad - 19)) < 0 ? 0 : Mathf.FloorToInt(movementSpeed * (Time.timeSinceLevelLoad - 19));
            scoreText.SetText("{0:0}", score);


            // This is for testing purpose so that we don't need a phone to activate swipe gestures
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                TurnLeft();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                TurnRight();
            }
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
        if (isMoving)
        {
            // Rotate 90 degrees when a swipe is detected
            if (data.Direction == SwipeDirection.Right)
            {
                TurnRight();
            }
            else if (data.Direction == SwipeDirection.Left)
            {
                TurnLeft();
            }
            else if (data.Direction == SwipeDirection.Up)
            {
                //GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

}
