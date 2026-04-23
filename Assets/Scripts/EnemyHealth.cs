using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    private Material currentMat;
    public float hitLenght;

    private float hitTimer;
    public GameObject hpText;
    public GameObject hpTextPos;
    public Material hitFx;

    protected override void Start()
    {
        base.Start();
        currentMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }    
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
    }

    public override bool Hurt(int damage)
    {
        base.Hurt(damage);
        GetComponentInChildren<SkinnedMeshRenderer>().material = hitFx;
        HpText txt = Instantiate(hpText, hpTextPos.transform.position, Quaternion.identity).GetComponent<HpText>();
        txt.GetComponent<TextMeshPro>().text = "-" + damage + " hp";

        return kill;
    }

    public override void Death()
    {
        FindFirstObjectByType<EnemiesQuest>().killedEnemies++;
        Destroy(gameObject);
    }
}