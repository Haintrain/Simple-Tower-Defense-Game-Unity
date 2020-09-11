using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour
{
    public List<GameObject> enemies;

    private Vector3 coords;

    public int maxResource = 10;
    public int usedResource = 0; //etc etc

    private void Start()
    {
        coords = new Vector3(0, 285, 0);

    }

    public void addUnit(int i)
    {
        //enemies[i]
        //get all the stuff from the units scriptable object
        //Transform unit = Instantiate(enemies[i]);
        // unit.SetParent(transform);
        // coords.y -= 45;
        // usedResource += 3;
        // unit.transform.localPosition = coords; //ignore all of this stuff lmao
        //queue.Enqueue(unit1);
    }
}
