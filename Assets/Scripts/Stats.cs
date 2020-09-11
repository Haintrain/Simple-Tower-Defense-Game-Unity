using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Stats : MonoBehaviour
{
    public Text resource, retires, dead, spawned, end, damage, spells;

    void Start()
    {
        Time.timeScale = 1;
        resource.text = "<b>Resources: </b>" + GameyManager.resourcesSpent;
        retires.text = "<b>Retries: </b>" + GameyManager.retries;
        
        damage.text = "<b>Damage: </b>" + GameyManager.damageTaken;
        spells.text = "<b>Spells: </b>" + GameyManager.spellsCast;

        dead.text = "<b>Dead Units</b>\n" +
            "\n Imp: " + GameyManager.enemiesDead[0] +
            "\n Priest: " + GameyManager.enemiesDead[1] +
            "\n Turtle: " + GameyManager.enemiesDead[2] +
            "\n Valkyrie: " + GameyManager.enemiesDead[3] +
            "\n Work Master: " + GameyManager.enemiesDead[4];

        spawned.text = "<b>Spawned Units</b>\n" +
            "\n Imp: " + GameyManager.enemiesSpawned[0] +
            "\n Priest: " + GameyManager.enemiesSpawned[1] +
            "\n Turtle: " + GameyManager.enemiesSpawned[2] +
            "\n Valkyrie: " + GameyManager.enemiesSpawned[3] +
            "\n Work Master: " + GameyManager.enemiesSpawned[4];

        end.text = "<b>Successful Units</b>\n" +
            "\n Imp: " + GameyManager.enemiesReachedEnd[0] +
            "\n Priest: " + GameyManager.enemiesReachedEnd[1] +
            "\n Turtle: " + GameyManager.enemiesReachedEnd[2] +
            "\n Valkyrie: " + GameyManager.enemiesReachedEnd[3] +
            "\n Work Master: " + GameyManager.enemiesReachedEnd[4];
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
