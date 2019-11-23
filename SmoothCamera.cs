using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float followSpeed = .3f;
    public float rotationSpeed = .3f;
    public float offsetHeight = 4;
    public float offsetBack = 6;

    float distance;
    Vector3 position;
    Vector3 newPos;
    Quaternion rotation;
    Quaternion newRot;

    // Use this for initialization
    void Start()
    {
        distance = transform.position.y - target.position.y;
        position = new Vector3(target.position.x, target.position.y + distance, target.position.z);
        rotation = Quaternion.Euler(new Vector3(0, target.rotation.eulerAngles.y, 0f));
    }

    void FixedUpdate()
    {
        // Only run if an object has been assigned to script
        if (target)
        {
            newPos = target.position;
            newPos.y += distance + offsetHeight;


            // Determine what direction to add offset to
            switch (target.GetComponent<Player>().relativeRotation / 90)
            {
                case 0:
                    newPos.z -= offsetBack;
                    break;

                case 1:
                    newPos.x -= offsetBack;
                    break;

                case 2:
                    newPos.z += offsetBack;
                    break;

                case 3:
                    newPos.x += offsetBack;
                    break;
            }

            // Change position and rotation with Slerp and Lerp so that they transform smoothly
            newRot = Quaternion.Euler(new Vector3(15f, target.rotation.eulerAngles.y, 0f));
            position = Vector3.Slerp(position, newPos, followSpeed * Time.deltaTime);
            rotation = Quaternion.Lerp(rotation, newRot, rotationSpeed * Time.deltaTime);
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
