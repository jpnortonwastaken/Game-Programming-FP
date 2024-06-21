using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public enum FSMStates {
        Idle,
        Shooting,
        Spawning,
        Chase,
        Attack,
        Dead
    }
    public FSMStates currentState;
    public Transform player;
    public float shootRate = 2;
    public float attackDistance = 2.5f;
    public GameObject bulletPrefab;
    public float moveSpeed = 5;
    //`Animator anim;
    float distanceToPlayer;
    public float elapsedTime=0;
    int health = 100;
    int currentDestinationIndex = 0;
    Transform deadTransform;
    Vector3 nextDestination;
    bool isDead;
    private int phase = 0;
    private int numberOfProjectiles = 20;
    private bool timeSet = false;
    public GameObject enemyPrefab;
    public GameObject projectilePrefab;
    public float spawnTime = 3;
    public float xMin = -25;
    public float yMin = 8;
    public float zMin = -25;
    public float xMax = 25;
    public float yMax = 25;
    public float zMax = 25;
    private int shootCount = 0;
    float attackTime;
    float chaseTime;
    float idleTime;
    float targetTime;
    private int spawnCount = 0;
    float projectileSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;    
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch(currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Shooting:
                UpdateShootingState();
                break;
            case FSMStates.Spawning:
                UpdateSpawningState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }    
    }

    void UpdateAttackState()
    {
        if (!timeSet) 
        {
            attackTime = 2f;
            timeSet = true;
        }
        attackTime -= Time.deltaTime;
        float step = moveSpeed * Time.deltaTime * 2.5f;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        if (attackTime <= 0.0f)
        {
            timeSet = false;
            currentState = FSMStates.Idle;
        }
    }

    void UpdateChaseState() 
    {
        if (!timeSet) 
        {
            chaseTime = 10f;
            timeSet = true;
        }
        chaseTime -= Time.deltaTime;
        float step = moveSpeed * Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        
        if(distanceToPlayer <= attackDistance){
            timeSet = false;
            currentState = FSMStates.Attack;
        }
        if (chaseTime <= 0.0f)
        {
            timeSet = false;
            currentState = FSMStates.Idle;
        }
    }

    void UpdateDeadState()
    {
        isDead = true;
        deadTransform = gameObject.transform;
    }

    void UpdateIdleState()
    {   
        if (!timeSet) 
        {
            if (phase == 0)
            {
                float idleTime = Random.Range(2,5);
            }
            else if (phase == 1)
            {
                float idleTime = Random.Range(6, 10);
            }
            timeSet = true;
        }
        if (idleTime <= 0.0f) {
            if (spawnCount <= 2 && shootCount <= 2) 
            {
                int randomInt = Random.Range(0, 1);
                if (randomInt == 0)
                {
                    timeSet = false;
                    currentState = FSMStates.Spawning;
                }
                if (randomInt == 1)
                {
                    timeSet = false;
                    currentState = FSMStates.Shooting;
                }
            }
            else
            {
                phase = 1;
                int randomInt = Random.Range(0, 2);
                if (randomInt == 0)
                {
                    timeSet = false;
                    currentState = FSMStates.Spawning;
                }
                if (randomInt == 1)
                {
                    timeSet = false;
                    currentState = FSMStates.Shooting;
                }
                if (randomInt == 2)
                {
                    timeSet = false;
                    currentState = FSMStates.Chase;
                }
            }
        }
        
    }

    void UpdateShootingState()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projectileVector = new Vector3(projectileDirX, projectileDirY, 0);
            Vector3 projectileMoveDirection = (projectileVector - transform.position).normalized * projectileSpeed;

            GameObject tempProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            tempProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
        Invoke(nameof(goToIdle), 2.0f);
    }

    void UpdateSpawningState()
    {
        if (!timeSet) 
        {
            timeSet = true;
            targetTime = 5f;
        }
        targetTime -= Time.deltaTime;
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
        if (targetTime <= 0.0f) {
            timeSet = false;
            currentState = FSMStates.Idle;
        }
    }

    void SpawnEnemies()
    {
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(xMin, xMax);
        enemyPosition.y = Random.Range(yMin, yMax);
        enemyPosition.z = Random.Range(zMin, zMax);
        GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation)
            as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            PlayerMovement.isKnockBacked = true;
            PlayerMovement.KnockBack();
            health -= 5;
            if (health == 0)
            {
                currentState = FSMStates.Dead;
            }
        }
    }

    void goToIdle()
    {
        currentState = FSMStates.Idle;
    }

}

