using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public Transform target;

    public float damage, aoeRadius, disable;
    public bool aoe, isPercentage;
    public Transform bloodPrefab;

    private float extraHeight = 0.5f;

    void Start() 
    {
        Vector3 direction = target.transform.position + Vector3.up * extraHeight - transform.position;
        this.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        Quaternion rotation = Quaternion.LookRotation(direction, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }

    void Update()
    {
        if (target != null) { 
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + Vector3.up * extraHeight, step);

            //rotate towards the target
            Vector3 direction = target.transform.position + Vector3.up * extraHeight - transform.position;
            if(direction != Vector3.zero) 
            {
                Quaternion rotation = Quaternion.LookRotation(direction, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
            
            if (transform.position == target.position + Vector3.up * extraHeight)
            {
                //print(disable);
                if (aoe)
                {
                    Utility.DamageAllEnemiesWithinRange(transform.position, aoeRadius, damage, bloodPrefab);
                    target = null;
                }
                else
                {
                    Transform bloodSplat = Instantiate(bloodPrefab, target);
                    EnemyAI enemy = target.GetComponent<EnemyAI>();
                    if(disable > 0)
                    {

                        enemy.disabled = true;
                        enemy.setDisabledTimer(disable);
                    }

                    if (isPercentage)
                    {
                        enemy.currentHealth -= enemy.enemy.Health * damage;
                        GameyManager.damageTaken += (int)(enemy.enemy.Health * damage);

                        target = null;
                    }
                    else
                    {
                        enemy.currentHealth -= damage;
                        GameyManager.damageTaken += (int)damage;
                        target = null;
                    }
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }
}
