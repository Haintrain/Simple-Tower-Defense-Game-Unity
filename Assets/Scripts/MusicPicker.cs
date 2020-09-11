using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPicker : MonoBehaviour
{
    public AudioClip NewMusic;
    public float volume;

    private AudioSource go;

    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MusicManager");
        if (obj != null)
        {
            go = obj.GetComponent<AudioSource>();
            if (go != null)
            {
                AudioListener.volume = this.volume * GameyManager.mainVolume; 
                if (go.clip != NewMusic)
                {
                    go.clip = NewMusic;
                    go.Play();
                }

            }
        }
    }
}
