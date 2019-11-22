using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPath : MonoBehaviour
{
    bool hasBeenActivated = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenActivated)
        {
            GameObject.FindGameObjectWithTag("PathGenerator").GetComponent<PathGenerator>().generatePath();
            hasBeenActivated = true;
        }
    }
}
