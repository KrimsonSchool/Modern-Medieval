using TMPro;
using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {currentMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        health = baseHealth;
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
    }

    public void Hurt(int damage)
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = hitFx;
        health -= damage;
        HpText txt = Instantiate(hpText, hpTextPos.transform.position, Quaternion.identity).GetComponent<HpText>();
        txt.GetComponent<TextMeshPro>().text = "-"+damage+" hp";
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
