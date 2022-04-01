using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyAI : MonoBehaviour
{
    private PlayerController playerController;
    private CreateWeapon createWeapon;

    private NavMeshAgent navMeshAgent;
   // private float changePosTime = 5f;
   // private float moveDistance = 40f;
    [SerializeField]
    private WeaponMarker weaponMarker;
    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange = 20f;

    //Attacking
    float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    //States
    float sightRange = 40f, attackRange = 30f;
    bool playerInSightRange, playerInAttackRange;
    
    [Inject]
    void Construct(PlayerController playerController, CreateWeapon createWeapon)
    {
        this.playerController = playerController;
        this.createWeapon = createWeapon;
    }
      

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        walkPoint = transform.position;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    void Patroling()
    {
        if (!walkPointSet) 
            SearchWalkPoint();
        if(walkPointSet)
            navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if(distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }

    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerController.transform.position);
    }
    void AttackPlayer()
    {
        //navMeshAgent.SetDestination(playerController.transform.position);
        transform.LookAt(playerController.transform);
        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            createWeapon.Create(weaponMarker);
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(
            transform.position.x + randomX, transform.position.y,transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}