using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public Tower tower;
    public List<Transform> enemies;
    private float stunCooldown = 3;
    private float stunTimer = 0;
    public Transform target;
    private Transform stunEffect;
    public Transform stunPrefab;
    private bool stunned = false;

    public bool stun = false;

    private float fireCooldown;
    public Transform bloodPrefab;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        //make this a thin targeting line or perhaps outline individual unit
        //and then make this just a line for wizard/ice tower...
        //Targeting line

        //todo: create a dotted line between every other tower and target (not essential)
        if(tower.name == "Wizard" || tower.name == "Ice Mage")
        {
            if (target != null)
            {
                lineRenderer.material.color = tower.name == "Wizard" ? Color.magenta : Color.cyan;
                lineRenderer.SetPosition(0, transform.position + Vector3.up * 1.25f);
                lineRenderer.SetPosition(1, target.position);
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }
        }
        

        if(stun==true)
        {
            target = null;
            stunTimer = stunCooldown;
            stun = false;
            if(!stunned)
            {
                stunned = true;
                stunEffect = Instantiate(stunPrefab, transform.position+new Vector3(0,1.75f,0), Quaternion.identity);
            }
            
        }
        if(stunTimer>0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }
        if(stunned && stunTimer<=0)
        {
            stunned = false;
            Destroy(stunEffect.gameObject);
        }
        if (target != null && Utility.Distance(transform.position, target.position) < tower.Range)
        {
            if (fireCooldown > 0)
            {
                fireCooldown -= Time.deltaTime;
            }
            else
            {
                if (tower.Targeting == 1)
                {
                    FindFirstTarget();
                }
                else if (tower.Targeting == 2)
                {
                    FindClosestTarget();
                }
                else if (tower.Targeting == 3)
                {
                    FindLastTarget();
                }

                if (tower.AoE)
                {
                    Utility.DamageAllEnemiesWithinRange(transform.position, tower.Range, tower.Damage, bloodPrefab);
                    fireCooldown = tower.FireCooldown;
                    StartCoroutine("projectileAoE");
                }
                else
                {
                    ShootProjectile();
                    if(this.tower.name == "Jailmaster")
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(97, 97, 97, 255);
                    }
                    fireCooldown = tower.FireCooldown;
                }
            }
        }
        else
        {
            if(tower.Targeting == 1)
            {
                FindFirstTarget();
            }
            else if(tower.Targeting == 2)
            {
                FindClosestTarget();
            }
            else if(tower.Targeting == 3)
            {
                FindLastTarget();
            }        
        }
    }

    void FindClosestTarget()
    {
        //Find easy way to convert gameobject list into transform
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Utility.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance && distanceToEnemy <= tower.Range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void FindFirstTarget()
    {
        //Find easy way to convert gameobject list into transform
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float longest = 0;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Utility.Distance(transform.position, enemy.transform.position);

            float distanceTravelled = enemy.GetComponent<EnemyAI>().distanceTravelled;
            if (distanceTravelled > longest && distanceToEnemy < tower.Range)
            {
                longest = distanceTravelled;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void FindLastTarget()
    {
        //Find easy way to convert gameobject list into transform
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortest = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Utility.Distance(transform.position, enemy.transform.position);

            float distanceTravelled = enemy.GetComponent<EnemyAI>().distanceTravelled;
            if (distanceTravelled < shortest && distanceToEnemy < tower.Range)
            {
                shortest = distanceTravelled;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void ShootProjectile()
    {
        //rotate the projectile
        //Vector3 direction = target.transform.position + Vector3.up * 1.5f - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        Transform projectile = Instantiate(tower.Projectile, transform.position+Vector3.up * 1.25f, rotation).transform;
        Projectiles proj = projectile.GetComponent<Projectiles>();
        //print(tower.Disable);
        //print(tower.Damage);
        proj.target = target;
        proj.damage = tower.Damage;
        proj.aoe = tower.AoE;
        proj.aoeRadius = tower.AoERadius;
        proj.disable = tower.Disable;
        proj.isPercentage = tower.PercentageDamage;
    }

    private void OnDrawGizmos()
    {
        //Targeting line
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
        }

        //Top down sphere range representation
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, tower.Range);
    }

    IEnumerator projectileAoE()
    {

        GameObject proj = Instantiate(tower.Projectile, transform);
        yield return new WaitForSeconds(0.25f);
        Destroy(proj);
    }
}
