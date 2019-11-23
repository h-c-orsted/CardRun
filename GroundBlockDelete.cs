using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockDelete : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(Time.time) > 19)
        {
            // Get diff of this block's position on x and z axis and player to determine if the player touches this block
            float xAxis = Mathf.Abs(transform.position.x - player.transform.position.x);
            float zAxis = Mathf.Abs(transform.position.z - player.transform.position.z);

            if (xAxis < 1.5 && zAxis < 1.5)
            {
                // Destroy path that we have already touched after 3 sec
                Destroy(gameObject, 3);
            }
        }
    }
}
