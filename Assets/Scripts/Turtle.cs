using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    private int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Hit(int dmg)
    {
        health -= dmg;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime);
        if(health==0)
        {
            Destroy(this.gameObject);
        }
    }
}
