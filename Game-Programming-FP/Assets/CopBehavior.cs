using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopBehavior : MonoBehaviour
{
    public enum FSMStates {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }
    public FSMStates currentState;
    public float attackDistance = 5;
    public float chaseDistance = 10;
    public GameObject player;
    public GameObject gunTip;
    public float shootRate = 2;
    public GameObject bulletPrefab;
    public float enemySpeed = 5;
    public Transform[] wanderPoints; 
    Animator anim;
    float distanceToPlayer;
    public float elapsedTime=0;
    int health;
    int currentDestinationIndex = 0;
    Transform deadTransform;
    Vector3 nextDestination;
    bool isDead;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        gunTip = GameObject.FindGameObjectWithTag("GunTip");
        Initialize();    
        isDead= false;    
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch(currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
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
        
        elapsedTime+= Time.deltaTime;       
    }
    void Initialize(){
        currentState = FSMStates.Patrol;
        FindNextPoint();
    
    }
    void UpdatePatrolState(){
        anim.SetInteger("animState", 1);
        if(Vector3.Distance(transform.position, nextDestination)<1){
            FindNextPoint();
        }else if (distanceToPlayer <= chaseDistance){
            currentState = FSMStates.Chase;
        }
        FaceTarget(nextDestination);
        // transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed*Time.deltaTime);
    }
    void UpdateChaseState(){
        anim.SetInteger("animState", 2);
        nextDestination = player.transform.position;
        if(distanceToPlayer <= attackDistance){
          currentState = FSMStates.Attack;
        }else if (distanceToPlayer > chaseDistance){
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed*Time.deltaTime);
    }
    void UpdateAttackState(){
        nextDestination = player.transform.position;
        if(distanceToPlayer <= attackDistance){
            currentState = FSMStates.Attack;
        }else if(distanceToPlayer>attackDistance&& distanceToPlayer <= chaseDistance) {
            currentState = FSMStates.Chase;
        }else if ( distanceToPlayer>chaseDistance){
            currentState = FSMStates.Patrol;
        }
        FaceTarget(nextDestination);
        anim.SetInteger("animState",3);
        ShootPlayer();
    }
    void UpdateDeadState(){
        anim.SetInteger("animState",4);
        isDead = true;
        deadTransform = gameObject.transform;
        // Destroy(gameObject,3);
    }
    void FindNextPoint(){
        if (wanderPoints.Length == 0){
            return;
        }
        nextDestination = wanderPoints[currentDestinationIndex].position;
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void FaceTarget(Vector3 target){
        Vector3 directionToTarget = (target-transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,10 * Time.deltaTime);
    }
    void ShootPlayer(){
        if(!isDead){
            if(elapsedTime>=shootRate){
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("Shooting",animDuration); 
                elapsedTime=0.0f;
            }
        }
    }
    void Shooting(){
        Instantiate(bulletPrefab, gunTip.transform.position,gunTip.transform.rotation);
    }
    private void OnDrawGizmos(){
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        //chase
        Gizmos.color = Color.green;
         Gizmos.DrawWireSphere(transform.position, chaseDistance);

    }
    private void OnDestroy(){
        // Instantiate(deadVFX,deadTransform.position, deadTransform.rotation);
    }
}
