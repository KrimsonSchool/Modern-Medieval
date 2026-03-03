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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetDist(FindFirstObjectByType<PlayerMovement>().transform.position);
    }

    // Update is called once per frame
    void Update()
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