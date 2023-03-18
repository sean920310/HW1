using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* reference: https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment*/

public class EnemyBot : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject player;
    private Rigidbody rb;

    private TankManager tankManager;

    public LayerMask playerMask;
    public LayerMask groundMask;

    public float enemySightRadius;
    public float walkRange;
    public float attackRange;
    public float sightFOV;

    public float idleTime;

    [Header("Information")]
    [ReadOnly]
    [SerializeField]
    private float idleCounter;

    [ReadOnly]
    [SerializeField]
    private Vector3 walkPoint;

    [ReadOnly]
    [SerializeField]
    private bool walkPointSet;

    private Vector3 oldPosition;

    [ReadOnly]
    [SerializeField]
    private bool playerInSight;
    [ReadOnly]
    [SerializeField]
    private bool playerInAttackRange;

    private Vector3 playerDir;
    private Vector3 playerDirEular;
    private Vector3 sightEular;
    [ReadOnly]
    [SerializeField]
    private Vector3 dirEularDelta;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        tankManager = transform.GetComponent<TankManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tankManager.health <= 0f )
            Died();

        // angle between player and enemy
        playerDir = (player.transform.position - transform.position).normalized;
        playerDirEular = Quaternion.LookRotation(playerDir, Vector3.up).eulerAngles;
        sightEular = transform.rotation.eulerAngles;
        dirEularDelta = (playerDirEular - sightEular);

        playerInSight = Physics.CheckSphere(transform.position, enemySightRadius, playerMask) && Mathf.Abs(dirEularDelta.y) < sightFOV;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        // get new walkpoint if stuck too long
        if((oldPosition - transform.position).magnitude < 1.0f)
        {
            idleCounter += Time.deltaTime;
            if(idleCounter >= idleTime)
            {
                idleCounter = 0.0f;
                walkPointSet = false;
            }
        }
        else
        {
            idleCounter = 0.0f;
        }

        if (!playerInSight) 
            Patroling();
        else if (playerInSight && !playerInAttackRange) 
            ChasePlayer();
        else
            AttackPlayer();

        oldPosition = transform.position;

        HeadLight(!TimeController.isDay);
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkRange, walkRange);
        float randomZ = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


        if (Physics.Raycast(walkPoint, -transform.up, 2.0f, groundMask))
            walkPointSet = true;
    }

    private void Patroling()
    {
        randomTowerRotation();

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
           
            if (agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                walkPointSet = false;
            }
            //agent.CalculatePath(walkPoint, nmp);
        }

        if ((transform.position - walkPoint).magnitude < 1.0f)
            walkPointSet = false;

        //if (Mathf.Abs(dirEularDelta.y) < 1.0f)
        //{
        //    // move forward
        //    tankManager.Move(1.0f, 0.0f);
        //}
        //else
        //{
        //    if (dirEularDelta.y > 0.0)
        //        tankManager.Move(0.0f, 1.0f);
        //    else if (dirEularDelta.y < 0.0)
        //        tankManager.Move(0.0f, -1.0f);
        //}
    }

    private void ChasePlayer()
    {
        walkPoint = player.transform.position;
        if(agent.isOnNavMesh)
            agent.SetDestination(walkPoint);

        towerRotation();
    }
    private void AttackPlayer()
    {
        walkPoint = transform.position;

        if (agent.isOnNavMesh)
            agent.SetDestination(walkPoint);

        tankManager.Fire();

        towerRotation();
    }

    private void randomTowerRotation()
    {

        tankManager.TowerAndCanonRotation(sightEular);
    }
    private void towerRotation()
    {
        tankManager.TowerAndCanonRotation(playerDirEular);
    }

    private void Died()
    {
        Instantiate(tankManager.explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void HeadLight(bool enable)
    {
        tankManager.headLightControl(enable);
    }
}
