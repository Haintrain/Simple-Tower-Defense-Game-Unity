using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool pauseActive;

    public GameObject GameEnd;
    public GameObject mainUI;

    private GameObject[] rangeObjects;

    void Start()
    {
        //Time.timeScale = PlayPauseFastforward.normalMax;
        //this is just to make it so i could add this script to restart level for alpha version
        if(pauseMenu != null)
        {
            pauseActive = false;
            pauseMenu.SetActive(pauseActive);
        }
        rangeObjects = GameObject.FindGameObjectsWithTag("Range");
        ToggleRadius();
    }

    void Update()
    {
        //todo make the button change on GUI for paused/play
        if(Input.GetKeyDown(KeyCode.Tab) && GameyManager.gameState != GameyManager.GameState.Tutorial)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseActive = !pauseActive;
        Time.timeScale = Time.timeScale == 0 ? PlayPauseFastforward.currentMax : 0;
        pauseMenu.SetActive(pauseActive);
    }

    public void MapMenu()
    {
        SceneManager.LoadScene("MapMenu");
    }

    public void Stats()
    {
        GameyManager.showStats = true;
        SceneManager.LoadScene("Stats");
    }

    public void Retry()
    {
        GameyManager.retries += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        pauseActive = !pauseActive;
        pauseMenu.SetActive(pauseActive);
        Time.timeScale = PlayPauseFastforward.currentMax;
    }

    public void Settings()
    {
        //todo
    }

    public void ToggleRadius()
    {
        foreach(GameObject obj in rangeObjects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
