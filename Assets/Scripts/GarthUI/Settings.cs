using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject play;
    public GameObject settings;
    public GameObject panel;

    public Slider volumeSlide;
    public Slider effectSlide;

    void Start()
    {
        settings.GetComponentInChildren<Text>().text = "Settings";
    }

    public void ToggleSettings()
    {
            settings.GetComponentInChildren<Text>().text  = settings.GetComponentInChildren<Text>().text == "Back" ? "Settings" : "Back";
            panel.SetActive(!panel.activeSelf);
            play.SetActive(!panel.activeSelf);
    }

    void Update()
    {
        
        if(settings.activeSelf)
        {
            GameyManager.mainVolume = volumeSlide.value;
            AudioListener.volume = GameyManager.mainVolume;
        }
    }
}
