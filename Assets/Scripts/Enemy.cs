using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    private Vector3 dest;
    public float aiDistanceMin;
    public float aiDistanceMax;

    private Animator animator;

    public int attackDamage;

    [HideInInspector] public bool patrol;
    [HideInInspector] public GameObject[] patrolPoints;
    private int patrolIndex;

    public float attackSpeed;


    private bool wander;
    private bool atWanderLoc=true;

    private Vector3 wanderDest;

    [HideInInspector] public bool chase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.speed = attackSpeed;
        
        SetDist(transform.position);
        if (FindFirstObjectByType<EnemiesQuest>() != null)
        {
            FindFirstObjectByType<EnemiesQuest>().noOfEnemies++;
        }

        foreach (GameObject p in patrolPoints)
        {
            p.GetComponent<MeshRenderer>().enabled = false;
            p.GetComponent<Collider>().enabled = false;
        }
        
        wanderDest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chase)
        {
            if (!patrol)
            {
                if (Vector3.Distance(transform.position, dest) <= aiDistanceMin)
                {
                    wander = false;
                    agent.SetDestination(transform.position);
                    animator.SetBool("IsMoving", false);
                    animator.SetBool("IsAttacking", true);
                }
                else if (Vector3.Distance(transform.position, dest) >= aiDistanceMax)
                {
                    //agent.SetDestination(transform.position);
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsMoving", false);

                    //wander
                    wander = true;
                }
                else
                {
                    wander = false;
                    agent.SetDestination(dest);
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("IsMoving", true);
                }

                dest = FindFirstObjectByType<PlayerMovement>().transform.position;
            }
            else
            {
                agent.SetDestination(patrolPoints[patrolIndex].transform.position);
                animator.SetBool("IsMoving", true);
                if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) <= 1)
                {
                    patrolIndex++;
                    if (patrolIndex >= patrolPoints.Length)
                    {
                        patrolIndex = 0;
                    }
                }
            }
        }
        else
        {
            patrol = false;
            aiDistanceMax = 999;
            dest = FindFirstObjectByType<PlayerMovement>().transform.position;
            
            chase = false;
            
            print("I want player, can i reach? " + IsDestinationReachable(dest));
        }



        if (wander)
        {
            if (!atWanderLoc)
            {
                print("Moving to loc");
                animator.SetBool("IsMoving", true);
                print(Vector3.Distance(transform.position, wanderDest));
                if (Vector3.Distance(transform.position, wanderDest)<=0.5f)
                {
                    print("at location");
                    atWanderLoc = true;
                }
            }
            else
            {
                print("Selecting location");
                animator.SetBool("IsMoving", false);
                Vector3 rng = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                wanderDest = transform.position + rng;
                if (IsDestinationReachable(wanderDest))
                {
                    agent.SetDestination(wanderDest);
                    atWanderLoc = false;
                }
                else
                {
                    atWanderLoc = true;
                }
            }
        }
    }


    public void SetDist(Vector3 distance)
    {
        dest = distance;
    }

    public void Attack()
    {
        FindFirstObjectByType<PlayerHealth>().Hurt(attackDamage);
    }
    
    public bool IsDestinationReachable(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
    
        if (agent.CalculatePath(targetPosition, path))
        {
            return path.status == NavMeshPathStatus.PathComplete;
        }
    
        return false;
    }
}