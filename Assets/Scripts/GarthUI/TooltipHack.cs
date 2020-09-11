using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHack : MonoBehaviour
{
    public GameObject tooltipObj;

    private SpawnManager spawnManager;

    public int barChoose;

    private BarScript bar;

    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnManager>();

        switch (barChoose)
        {
            case 0:
                bar = GameObject.FindGameObjectWithTag("Resources").GetComponent<BarScript>();
                break;
            case 1:
                bar = GameObject.FindGameObjectWithTag("Spells").GetComponent<BarScript>();
                break;
        }
    }

    public void ToggleOn()
    {
        tooltipObj.SetActive(true);
    }

    public void ToggleOff()
    {
        tooltipObj.SetActive(false);
    }

    public void hoverUnit(int unit)
    {
        int cost = 0;
        switch (barChoose)
        {
            case 0:
                cost = spawnManager.enemies[unit].GetComponent<EnemyAI>().enemy.UnitCost;
                break;
            case 1:
                cost = 100;
                break;
        }
        bar.UpdateHover(cost, true);
    }

    public void unhoverUnit(int unit)
    {
        bar.UpdateHover(0, false);
    }
}
