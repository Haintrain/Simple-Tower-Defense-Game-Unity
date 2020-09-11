using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSoundChange : MonoBehaviour
{

    AudioSource audioSauce;
    float min;
    float max;

    float duration = 2;
    float startTime;

    void Start()
    {
        min = 0.2f;
        startTime = Time.time;
        GameObject obj = GameObject.FindGameObjectWithTag("MusicManager");
        audioSauce = obj.GetComponent<AudioSource>();
        max = audioSauce.volume;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        audioSauce.volume = Mathf.SmoothStep(max, min, t);

        if((Time.time - startTime) > 5)
        {
            audioSauce.volume = max;
        }
    }
}
