using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn;
    

    public float delay;
    public BasicNodePath nodePath;

    private int queueSize = 40;

    private Transform enemy;

    public List<GameObject> enemies;
    public List<Button> unitButtons;

    public List<Transform> queueUnits;
    private List<int> oldQueueUnits = new List<int>();

    public Transform queueItem;
    public Transform wholeQueue;
    private Vector3 coords;

    public GameObject gameLose;
    private Objective objective;

    public BarScript bar;

    public PlayerController playerController;

    public void Start()
    {
        enemiesToSpawn = new List<GameObject>();
        nodePath = GameObject.FindGameObjectWithTag("NodeManager").GetComponent<BasicNodePath>();

        coords = new Vector3(0, 290,0);

        GameObject objectiveGO = GameObject.FindGameObjectWithTag("Objective");
        objective = objectiveGO.GetComponent<Objective>();
        

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            StartCoroutine("SpawnMenu");
        }
    }

    void Update()
    {         
        //Checks if all enemies are dead.
        if(GameyManager.gameState == GameyManager.GameState.Play)
        {
            if(enemiesToSpawn.Count == 0 && GameObject.FindGameObjectWithTag("Enemy") == null && objective.health > 0)
            {
                GameyManager.gameState = GameyManager.GameState.End;
                gameLose.gameObject.SetActive(true);

                Text text = gameLose.GetComponentInChildren<Text>();
                if(GameyManager.levelResources > 1)
                {
                    text.text = "Hmm.. I do have " + GameyManager.levelResources + " resources left over.\n\nMaybe there is something I can do with that?";
                } else if (GameyManager.levelMana >= 100)
                {
                    text.text = "No no no...\n\nWhat if i tried to cast another spell?";
                } else {
                    text.text = "Rats!\n\nI needed " + objective.health + " more damage.\n\nMaybe a different unit will help...";
                }

                for(int i = 0; i < enemies.Count; i++)
                {
                    unitButtons[i].interactable = false;
                }

                foreach(int enemyNo in oldQueueUnits)
                {
                    AddToQueueHistory(enemyNo);
                }
                //it does retries from the retry button anyway
                //GameyManager.retries += 1;
            }
        }

        if (objective.health <= 0 && oldQueueUnits.Count != 0)
        {
            for(int i = 0; i < enemies.Count; i++)
                {
                unitButtons[i].interactable = false;
            }

            foreach (int enemyNo in oldQueueUnits)
            {
                AddToQueueHistory(enemyNo);
            }
            oldQueueUnits.Clear();
        }
    }

    private void AddToQueueHistory(int enemyNo)
    {
        //enemiesToSpawn.Add(enemies[enemyNo]);
        Transform unit = Instantiate(queueItem);
        unit.SetParent(wholeQueue);
        coords.y -= queueSize;
        unit.transform.localPosition = coords;
        Image imageComponent = unit.GetComponent<Image>();
        imageComponent.sprite = enemies[enemyNo].GetComponent<SpriteRenderer>().sprite;
    }

    public void AddToQueue(int enemy)
    {     
        if(GameyManager.gameState != GameyManager.GameState.Play)
        {
            if(enemies[enemy].GetComponent<EnemyAI>().enemy.UnitCost <= GameyManager.levelResources)
            {
                oldQueueUnits.Add(enemy);
                enemiesToSpawn.Add(enemies[enemy]);
                Transform unit = Instantiate(queueItem);
                unit.SetParent(wholeQueue);
                coords.y -= queueSize;
                unit.transform.localPosition = coords;
                Image imageComponent = unit.GetComponent<Image>();
                imageComponent.sprite = enemies[enemy].GetComponent<SpriteRenderer>().sprite;
                GameyManager.levelResources -= enemies[enemy].GetComponent<EnemyAI>().enemy.UnitCost;

                queueUnits.Add(unit);

                bar.UpdateImage();
            }

            for(int i=0; i<enemies.Count; i++)
            {
                if(enemies[i].GetComponent<EnemyAI>().enemy.UnitCost > GameyManager.levelResources)
                {
                    unitButtons[i].interactable = false;
                }
            }
        }
    }

    public void StartSpawning()
    {
        // if(Time.timeScale != PlayPauseFastforward.normalMax) Time.timeScale = PlayPauseFastforward.normalMax;
        //stops the player from spawning multiple waves.
        if(GameyManager.gameState == GameyManager.GameState.Play || enemiesToSpawn.Count == 0)
        {
            return;
        } else if (GameyManager.gameState != GameyManager.GameState.End) {
            GameyManager.gameState = GameyManager.GameState.Play;
            StartCoroutine("SpawnQueued");
            playerController.EnableSpells();
        }
        
    }

    IEnumerator SpawnQueued()
    {
        while(enemiesToSpawn.Count > 0)
        {
            enemy = Instantiate(enemiesToSpawn[0], nodePath.startNode.transform.position, Quaternion.identity).transform;

            switch (enemy.GetComponent<EnemyAI>().enemy.enemyName)
            {
                case "Imp":
                    GameyManager.enemiesSpawned[0] += 1;
                    GameyManager.resourcesSpent += enemy.GetComponent<EnemyAI>().enemy.UnitCost;
                    break;
                case "Priest":
                    GameyManager.enemiesSpawned[1] += 1;
                    GameyManager.resourcesSpent += enemy.GetComponent<EnemyAI>().enemy.UnitCost;
                    break;
                case "Turtle":
                    GameyManager.enemiesSpawned[2] += 1;
                    GameyManager.resourcesSpent += enemy.GetComponent<EnemyAI>().enemy.UnitCost;
                    break;
                case "Valkyrie":
                    GameyManager.enemiesSpawned[3] += 1;
                    GameyManager.resourcesSpent += enemy.GetComponent<EnemyAI>().enemy.UnitCost;
                    break;
                case "Work Master":
                    GameyManager.enemiesSpawned[4] += 1;
                    GameyManager.resourcesSpent += enemy.GetComponent<EnemyAI>().enemy.UnitCost;
                    break;
            }
            
            enemiesToSpawn.RemoveAt(0);

            wholeQueue.localPosition += Vector3.up * queueSize;
            GameyManager.spawnedEnemies.Add(enemy);

            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            enemyScript.Target = nodePath.startNode.GetComponent<Node>().nextNode;
            enemyScript.nodePath = this.nodePath;

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SpawnMenu()
    {
        while (true)
        {
            enemy = Instantiate(enemies[Random.Range(0, 5)], nodePath.startNode.transform.position, Quaternion.identity).transform;
            Destroy(enemy.GetComponentInChildren<Canvas>().gameObject);
            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            enemyScript.Target = nodePath.startNode.GetComponent<Node>().nextNode;
            enemyScript.nodePath = this.nodePath;

            yield return new WaitForSeconds(delay);

        }
    }

    public void UndoQueue()
    {
        if(enemiesToSpawn.Count == 0) return;
        
        GameyManager.levelResources += enemiesToSpawn[enemiesToSpawn.Count - 1].GetComponent<EnemyAI>().enemy.UnitCost;

        Destroy(queueUnits[queueUnits.Count - 1].gameObject);
            
        queueUnits.RemoveAt(queueUnits.Count - 1);
        enemiesToSpawn.RemoveAt(enemiesToSpawn.Count - 1);
        oldQueueUnits.RemoveAt(oldQueueUnits.Count - 1);

        coords.y += queueSize;

        bar.UpdateImage();

        for(int i=0; i<enemies.Count; i++)
        {
            if(enemies[i].GetComponent<EnemyAI>().enemy.UnitCost <= GameyManager.levelResources)
            {
                if(unitButtons[i].name != "disabled") 
                {
                    unitButtons[i].interactable = true;
                }
            }
        }
    }

    public void ResetQueue()
    {
        GameyManager.levelResources = GameyManager.resourcesMax;

        coords.y += queueUnits.Count * queueSize;

        foreach(Transform unit in queueUnits)
        {
            Destroy(unit.gameObject);
        }

        queueUnits.Clear(); 
        enemiesToSpawn.Clear(); 
        oldQueueUnits.Clear();

        bar.UpdateImage();
    }
}
