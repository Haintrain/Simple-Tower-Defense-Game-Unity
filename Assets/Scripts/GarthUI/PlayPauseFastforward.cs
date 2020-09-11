using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseFastforward : MonoBehaviour
{
    public static float currentMax;
    public static float normalMax;
    
    public Text play;
    public Text fast;

    void Start()
    {
        normalMax = 0.5f;
        currentMax = normalMax;
        play.text = "Play";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameyManager.levelResources == GameyManager.resourcesMax) return;
        if(play.text == "Play")
        {
            play.text = ">";
            //return;
        }
        play.text = play.text == ">" ? "||" : ">";
        Time.timeScale = play.text == ">" ? 0 : currentMax;
    }

    public void ToggleFast()
    {
        currentMax = currentMax == normalMax ? normalMax*3 : normalMax;
        Time.timeScale = Time.timeScale == 0 ? 0 : currentMax;
        fast.text = fast.text == ">>" ? "[>>]" : ">>";
    }

}
