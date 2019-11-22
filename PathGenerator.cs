using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{

    public GameObject groundPrefab;
    public GameObject triggerPrefab;
    public GameObject cardPrefab;


    int rotation = 0;
    public Quaternion pathRotation = Quaternion.Euler(0, 0, 0);
    public Vector3 instancePosition = new Vector3(0, 0, 0);


    void Start()
    {
        generatePath(150, false);
        Instantiate(cardPrefab, new Vector3(50, 0, 0), Quaternion.Euler(0,0,0));
    }



    public void generatePath(int count = 50, bool rotationPossible = true)
    {
        // Is able to deactivate rotation. Used for the first generated blocks
        if (rotationPossible)
        {
            // Get random number between -1 and 1 (2 is because of the random function that's behaving .... randomly) to determine rotation (left, straigt or right)
            int randomNumber = Random.Range(-1, 2);
            int newRotation = Mathf.RoundToInt(pathRotation.y) + randomNumber;

            Debug.Log(randomNumber);
            Debug.Log(newRotation);

            // Update rotation quaternion and variable
            pathRotation = Quaternion.Euler(0, newRotation * 90, 0);

            if (rotation != newRotation)
            {
                // Generate fill. Subtract 1 from instance position to get position of fill block
                switch (newRotation)
                {
                    case -1:
                        Instantiate(groundPrefab, instancePosition - new Vector3(0, 0, 1), pathRotation);
                        break;

                    case 0:
                        Instantiate(groundPrefab, instancePosition - new Vector3(1, 0, 0), pathRotation);
                        break;

                    case 1:
                        Instantiate(groundPrefab, instancePosition - new Vector3(0, 0, -1), pathRotation);
                        break;

                }

                rotation = newRotation;
            }
        }


        // Generate blocks
        for (int i=0; i<count; i++)
        {
            // Every 50 block, place a trigger block. Else place normal non-trigger block
            if (i % 50 == 0)
            {
                Instantiate(triggerPrefab, instancePosition, pathRotation);
            }
            else
            {
                Instantiate(groundPrefab, instancePosition, pathRotation);
            }


            // Add 1 to instance position in whatever direction the rotation is
            switch (rotation)
            {
                case -1:
                    instancePosition += new Vector3(0, 0, 1);
                    break;

                case 0:
                    instancePosition += new Vector3(1, 0, 0);
                    break;

                case 1:
                    instancePosition += new Vector3(0, 0, -1);
                    break;

            }
        }
    }
}
