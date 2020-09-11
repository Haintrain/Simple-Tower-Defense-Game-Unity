using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject statsButton;

    public void Start()
    {
        if (GameyManager.showStats)
        {
            statsButton.SetActive(true);
        }
        else
        {
            statsButton.SetActive(false);
        }
    }

    public void MapMenu()
    {
        SceneManager.LoadScene("MapMenu");
    }

    public void Stats()
    {
        SceneManager.LoadScene("Stats");
    }


    //either add a controls menu or settings menu :)
    // public void Controls()
    // {
    //     SceneManager.LoadScene("Controls");
    // }

    public void Quit()
    {
        Application.Quit();
    }
}
