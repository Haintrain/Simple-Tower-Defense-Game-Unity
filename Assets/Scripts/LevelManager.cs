using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameyManager;

public class LevelManager : MonoBehaviour
{
    public int levelResources;
    public int levelMana;
    public float manaRestoreRate;

    public bool isTutorial;

    public Text resources;
    public Text mana;

    public GameObject undo;

    public int lvl;

    void Start()
    {
        
        if(GameyManager.visitedLevels.Contains(SceneManager.GetActiveScene().name))
        {
            gameState = GameState.Queue;
        } else
        {
            GameyManager.gameState = isTutorial == true ? GameState.Tutorial : GameState.Queue;
            GameyManager.visitedLevels.Add(SceneManager.GetActiveScene().name);
        }
        
        // if(isTutorial)
        // {
        //     tutorialDone = false;
        // }
        

        GameyManager.levelMana = this.levelMana;
        GameyManager.levelResources = this.levelResources;
        GameyManager.manaMax = this.levelMana;
        GameyManager.resourcesMax = this.levelResources;
    }

    private void Update()
    {
        if (resources != null)
        {
            resources.text = GameyManager.levelResources.ToString();
        }

        if(mana != null)
        {
            mana.text = GameyManager.levelMana.ToString();
        }

        if(gameState == GameState.Play && undo.activeSelf == true)
        {
            undo.SetActive(false);
        }

        if((gameState == GameState.Queue || gameState == GameState.Tutorial) && undo.activeSelf == false)
        {
            undo.SetActive(true);
        }
    }
}
