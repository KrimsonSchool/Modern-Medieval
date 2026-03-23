using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public enum EnemyTypes
    {
        None,Grunt,Soldier,Tough
    }

    public EnemyTypes enemyTypes;
    WorldManager worldManager;

    public bool random;
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
        if (index <2)
        {
            Instantiate(worldManager.enemies[index], transform.position, transform.rotation);
        }
    }
}
