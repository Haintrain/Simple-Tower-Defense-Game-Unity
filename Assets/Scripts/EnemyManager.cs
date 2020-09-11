using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Transform> spawnedEnemies;

    public BasicNodePath nodePath;

    private Transform enemy;

    public void Start()
    {
        nodePath = GameObject.Find("/NodeManager").GetComponent<BasicNodePath>();
    }

    public void Spawn()
    {
        if (GameyManager.levelResources > 0)
        {
            enemy = Instantiate(enemies[0], nodePath.startNode.transform.position, Quaternion.identity).transform;

            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            enemyScript.Target = nodePath.startNode.GetComponent<Node>().nextNode;
            enemyScript.nodePath = this.nodePath;

            spawnedEnemies.Add(enemy);
            GameyManager.levelResources -= enemy.GetComponent<EnemyAI>().enemy.UnitCost;
        }
    }

    public void Spawn1()
    {
        if (GameyManager.levelResources > 0)
        {
            enemy = Instantiate(enemies[1], nodePath.startNode.transform.position, Quaternion.identity).transform;

            EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
            enemyScript.Target = nodePath.startNode.GetComponent<Node>().nextNode;
            enemyScript.nodePath = this.nodePath;

            spawnedEnemies.Add(enemy);
            GameyManager.levelResources -= enemy.GetComponent<EnemyAI>().enemy.UnitCost;
        }
    }
}
