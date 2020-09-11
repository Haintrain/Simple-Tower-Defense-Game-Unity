using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MapMenu : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();

    private string[] unlockString;
    private int index;


    void Start()
    {
        unlockString = new string[] { "g", "o", "r", "t"};
        index = 0;

        Time.timeScale = PlayPauseFastforward.normalMax;
        GameyManager.gameState = GameyManager.GameState.Menu;
        if(GameyManager.levelsCompleted != levels.Count)
        {
            levels[GameyManager.levelsCompleted].GetComponentsInChildren<Image>()[1].enabled = false;
        }
        
        for(int i=GameyManager.levelsCompleted+1; i<levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(unlockString[index]))
            {
                index++;
                if (index == unlockString.Length)
                {
                    UnlockAll();
                }
            } else
            {
                index = 0;
            }
        }
    }

    public void Tutorial(int value)
    {
        SceneManager.LoadScene("Tutorial_"+value);
    }

    public void Level(int value)
    {
        SceneManager.LoadScene("Level_"+value);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UnlockAll()
    {
        if(GameyManager.levelsCompleted != levels.Count)
        {
            GameyManager.levelsCompleted = levels.Count;
        } else
        {
            GameyManager.levelsCompleted = 0;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
