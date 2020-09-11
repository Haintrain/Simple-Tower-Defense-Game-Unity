using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DancingImage : MonoBehaviour
{

    public float danceSpeed;
    private float timeSinceLastFrame;

    // Update is called once per frame
    void Update()
    {
        // Add time since last game frame to time since last
        // animation frame change
        timeSinceLastFrame += Time.deltaTime;
        //Debug.Log(timeSinceLastFrame);
        //Debug.Log(Time.timeScale);


        float rotatate = transform.rotation.y > 0 ? 0 : 180;

        //Check if it's time for animation frame change
        if (timeSinceLastFrame > 1f * danceSpeed)
        {

            transform.rotation = new Quaternion(0, rotatate, 0, 0);
            timeSinceLastFrame = 0;
        }
    }
}
