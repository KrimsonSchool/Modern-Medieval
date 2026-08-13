using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public enum EnemyTypes
    {
        None,
        Standard,
        Patrol
    }

    public EnemyTypes enemyTypes;
    WorldManager worldManager;

    public bool random;

    [HideInInspector] public GameObject[] patrolPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldManager = FindFirstObjectByType<WorldManager>();

        if (random)
        {
            Spawn(Random.Range(0, Enum.GetNames(typeof(EnemyTypes)).Length));
        }
        else
        {
            Spawn((int)enemyTypes);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Spawn(int index)
    {
        Enemy enemy = Instantiate(worldManager.enemies[index], transform.position, transform.rotation).GetComponent<Enemy>();
        
        if (enemyTypes == EnemyTypes.Patrol)
        {
            enemy.patrol = true;
            //print("moving [" + patrolPoints.Length+"] points to enemy");
            enemy.patrolPoints = new GameObject[patrolPoints.Length];
            enemy.patrolPoints = patrolPoints;
        }
    }
}