using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public BasicNodePath nodePath;
    public Enemy enemy;
    public Transform disablePrefab;

    public bool speedBuff = false;
    private float speedBuffTimer = 0;
    private float speedBuffCooldown = 3;

    public float currentHealth, speed, distanceTravelled = 0;

    //used to flip sprite for different directions of movement
    private Vector3 lastPos;
    private Vector3 oldVelocity;
    private Vector3 velocity;
    public float unitSpeedBuffTimer = 1f;

    public bool disabled, isHealer, isSpeeder, unitSpeedBuff;

    public Image HP;
    public Text nameField;

    [SerializeField]
    private Transform target;
    private float healCooldown = 0, healRadii, healAmount, disabledTimer = 0, speedAmount, checkCooldown = 0;
    private List<GameObject> enemies;
    public Transform healPrefab;
    private List<GameObject> firstFourEnemies;
    private bool disabledEffect;
    private Transform disableEffect;

    public Transform Target { get { return target; } set { target = value; } }

    public void setDisabledTimer(float time)
    {
        disabledTimer = time;
    }

    // Start is called before the first frame update
    void Start()
    {
        nodePath = GameObject.FindGameObjectWithTag("NodeManager").GetComponent<BasicNodePath>();
        target = nodePath.startNode;
        enemies = new List<GameObject>();
        firstFourEnemies = new List<GameObject>();

        currentHealth = enemy.Health;
        speed = enemy.Speed;
        nameField.text = enemy.enemyName;
        isHealer = enemy.IsHealer;
        isSpeeder = enemy.IsSpeeder;
        healCooldown = enemy.HealCooldown;
        healRadii = enemy.HealRadii;
        healAmount = enemy.HealAmount;
        disabledEffect = false;

        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (unitSpeedBuff)
        {
            speed = enemy.Speed * 1.2f;
        }
        else
        {
            speed = enemy.Speed;
        }
        if (speedBuff == true)
        {
            speedBuffTimer = speedBuffCooldown;
            speedBuff = false;
            speed = enemy.Speed * 2;
        }
        if (speedBuffTimer > 0)
        {
            speedBuffTimer -= Time.deltaTime;
            if(speedBuffTimer<=0)
            {
                speed = enemy.Speed;
            }
        }*/

        if(unitSpeedBuffTimer>0)
        {
            unitSpeedBuffTimer -= Time.deltaTime;
        } else
        {
            unitSpeedBuff = false;
        }
        if (speedBuff)
        {
            speedBuffTimer = speedBuffCooldown;
            speedBuff = false;
            
            if (unitSpeedBuff)
            {
                speed = enemy.Speed * 3f;
            }
            else
            {
                speed = enemy.Speed * 2;
            }
        }
        else if (speedBuffTimer > 0)
        {
            speedBuffTimer -= Time.deltaTime;
        }
        else if (unitSpeedBuff)
        {
            speed = enemy.Speed * 2f;
        }
        else
        {
            speed = enemy.Speed;
        }

        HP.fillAmount = currentHealth / enemy.Health;

        if(nodePath.endNode == null)
        {
            //Change animation to a dancing animation
            return;
        }

        if (currentHealth <= 0)
        {
            GameyManager.spawnedEnemies.Remove(transform);

            switch (enemy.enemyName)
            {
                case "Imp":
                    GameyManager.enemiesDead[0] += 1;
                    break;
                case "Priest":
                    GameyManager.enemiesDead[1] += 1;
                    break;
                case "Turtle":
                    GameyManager.enemiesDead[2] += 1;
                    break;
                case "Valkyrie":
                    GameyManager.enemiesDead[3] += 1;
                    break;
                case "Work Master":
                    GameyManager.enemiesDead[4] += 1;
                    break;
            }

            Destroy(this.gameObject);
        }

        if(target == null)
        {
            //Debug.Log("No target");
            return;
        }

        if (transform.position != target.transform.position)
        {
            float step = speed * Time.deltaTime;
            distanceTravelled += step;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        else
        {
            if (target == nodePath.endNode && nodePath.endNode.GetComponent<Objective>() != null)
            { 
                nodePath.endNode.GetComponent<Objective>().health -= enemy.Damage;

                switch (enemy.enemyName)
                {
                    case "Imp":
                        GameyManager.enemiesReachedEnd[0] += 1;
                        break;
                    case "Priest":
                        GameyManager.enemiesReachedEnd[1] += 1;
                        break;
                    case "Turtle":
                        GameyManager.enemiesReachedEnd[2] += 1;
                        break;
                    case "Valkyrie":
                        GameyManager.enemiesReachedEnd[3] += 1;
                        break;
                    case "Work Master":
                        GameyManager.enemiesReachedEnd[4] += 1;
                        break;
                }

                GameyManager.spawnedEnemies.Remove(transform);
                Destroy(this.gameObject);
            }
            else
            {
                target = target.GetComponent<Node>().nextNode;
            }
        }

        if (disabled)
        {
            /*Transform canvas = transform.GetChild(0);
            Outline parentOutline = canvas.GetComponentInChildren<Outline>();
            Outline outline = parentOutline.GetComponentsInChildren<Outline>()[1];
            outline.effectColor = new Color32(100, 240, 240, 255);*/
            if(!disabledEffect)
            {
                disableEffect = Instantiate(disablePrefab, transform);
                disabledEffect = true;
            }
            

            if (disabledTimer <= 0)
            {
                disabled = false;
                disabledEffect = false;
                Destroy(disableEffect.gameObject);
                //outline.effectColor = new Color32(0, 0, 0, 255);
            }
            else
            {
                disabledTimer -= Time.deltaTime;
            }
        }
        else if (isHealer)
        {
            if (healCooldown <= 0)
            {
                Utility.DamageAllEnemiesWithinRange(transform.position, healRadii, healAmount, healPrefab);
                healCooldown = enemy.HealCooldown;
            }
            else
            {
                healCooldown -= Time.deltaTime;
            }
        }
        else if (isSpeeder)
        {
            if (checkCooldown <= 0)
            {
                enemies.Clear();
                enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                //enemies.Remove(this.gameObject);
                enemies = enemies.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();

                foreach (GameObject e in enemies.Take(Mathf.Min(enemies.Count, 4)))
                {
                    //Debug.Log("Test");
                    e.GetComponent<EnemyAI>().unitSpeedBuff =true;
                    e.GetComponent<EnemyAI>().unitSpeedBuffTimer = 1;
                }

                /*foreach (GameObject e in enemies.Skip(4))
                {
                    e.GetComponent<EnemyAI>().unitSpeedBuff = false;
                }*/

                checkCooldown = .5f;
            }
            else
            {
                checkCooldown -= Time.deltaTime;
            }
        }

        //lastPos
        //oldVelocity
        //velocity
        velocity = transform.position - lastPos;
        if (transform.position == lastPos) return;
        lastPos = transform.position;
        //checks if velocity and oldVelocity has had a changed sign bit
        
        if((velocity.x *oldVelocity.x) >= 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = !(velocity.x > 0);
        }
        oldVelocity = velocity;
    }
}
