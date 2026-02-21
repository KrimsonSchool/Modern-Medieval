using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int baseHealth;
    int health;

    public GameObject hpText;
    public GameObject hpTextPos;

    public Material hitFx;
    private Material currentMat;
    public float hitLenght;

    private float hitTimer;

    NavMeshAgent agent;
    private Vector3 dest;
    public float aiDistance;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        health = baseHealth;
        agent = GetComponent<NavMeshAgent>();
        animator  = GetComponent<Animator>();
        SetDist(FindFirstObjectByType<PlayerMovement>().transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<SkinnedMeshRenderer>().material != currentMat)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitLenght)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material = currentMat;
                hitTimer = 0;
            }
        }

        if (Vector3.Distance(transform.position, dest) <= aiDistance)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("IsMoving", false);
        }
        else
        {
            agent.SetDestination(dest);
            animator.SetBool("IsMoving", true);
        }
        
        dest = FindFirstObjectByType<PlayerMovement>().transform.position;
    }

    public void Hurt(int damage)
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = hitFx;
        health -= damage;
        HpText txt = Instantiate(hpText, hpTextPos.transform.position, Quaternion.identity).GetComponent<HpText>();
        txt.GetComponent<TextMeshPro>().text = "-" + damage + " hp";

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetDist(Vector3 distance)
    {
        dest = distance;
    }
}