using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    public float health;

    private float maxHealth;

    public Image HP;

    //public GameObject PauseEnd;
    public GameObject UI, GameEnd;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        if (GameyManager.gameState != GameyManager.GameState.Menu)
        {
            maxHealth = health;
            //UI = GameObject.FindWithTag("MainUI");
            //PauseEnd = GameObject.FindWithTag("PauseUI");
            //GameEnd = GameObject.FindWithTag("EndUI");
            Transform child = GameEnd.GetComponentsInChildren<Transform>(true)[1];
            child.gameObject.SetActive(false);
        }
        if(UI == null)
        {
            Debug.Log("no UI found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameyManager.gameState != GameyManager.GameState.Menu)
        {
            HP.fillAmount = health / maxHealth;

            if (health <= 0)
            {
                GameyManager.gameState = GameyManager.GameState.End;
                //UI.SetActive(false);

                Transform child = GameEnd.GetComponentsInChildren<Transform>(true)[1];
                child.gameObject.SetActive(true);
                Text winText = GameEnd.GetComponentInChildren<Text>(true);

                switch(Random.Range(1,5))
                {
                    case 1:
                        winText.text = "Nothing but rubble, a shame really";
                        break;
                    case 2:
                        winText.text = "My beautiful creations cannot be stopped!";
                        break;
                    case 3:
                        winText.text = "Cower before my might!";
                        break;
                    case 4:
                        winText.text = "First we take over Jeffs' house...\n\nthen we take over the world!!";
                        break;

                    default:
                        winText.text = "Mwahahahaha";
                        break;
                }

                //stop repeated completions
                string levelname = SceneManager.GetActiveScene().name;
                int add = levelname.Contains("Tutorial") ? 0 : 4;

                if (GameyManager.levelsCompleted < int.Parse(levelname.Substring(levelname.Length - 1)) + add)
                {
                    GameyManager.levelsCompleted += 1;
                }

                //enable fire effect... or some animation of a house burning down
                Destroy(this.gameObject);
            }
        }
    }
}
