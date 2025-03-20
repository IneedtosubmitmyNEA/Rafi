using UnityEngine;
using UnityEngine.AI;
public class Enemyscript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer; 
    public GameObject weaponHolder;
    //patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() // due to having lots of enemies, manually assigining the player will be difficult
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    

    private void Update()
    {
        //checks if player is in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patrolling();// patrol if out of both spheres
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();//chase if in one sphere and not the other
        if(playerInSightRange && playerInAttackRange) AttackPlayer(); // attack if in both spheres
    }

    private void Patrolling()
    {
        if (!walkPointSet) {SearchWalkPoint();}

        if (walkPointSet)
        {agent.SetDestination(walkPoint);}// makes the ai walk to that point

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {walkPointSet = false;}
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {walkPointSet = true;}
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);// makes the AI move toward the player
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);// stops the AI from moving

        transform.LookAt(player);// orients the Ai to look at the player

        if (!alreadyAttacked)
        {
            ///Attack code 
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); //nameof just converts to string
        }// lets the Ai attacl the player after timeBetweenAttacks
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
