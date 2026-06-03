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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetDist(transform.position);
        if (FindFirstObjectByType<EnemiesQuest>() != null)
        {
            FindFirstObjectByType<EnemiesQuest>().noOfEnemies++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!patrol)
        {
            if (Vector3.Distance(transform.position, dest) <= aiDistanceMin)
            {
                agent.SetDestination(transform.position);
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
            }
            else if (Vector3.Distance(transform.position, dest) >= aiDistanceMax)
            {
                agent.SetDestination(transform.position);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsMoving", false);
            }
            else
            {
                agent.SetDestination(dest);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsMoving", true);
            }

            dest = FindFirstObjectByType<PlayerMovement>().transform.position;
        }
        else
        {
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
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


    public void SetDist(Vector3 distance)
    {
        dest = distance;
    }

    public void Attack()
    {
        //TODO change to generic health system, have enemy have Targeted object which is used here
        FindFirstObjectByType<PlayerHealth>().Hurt(attackDamage);
    }
}