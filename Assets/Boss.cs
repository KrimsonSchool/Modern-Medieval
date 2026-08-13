using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    private int _maxHealth;

    public Slider healthSlider;
    public Slider healthSliderVis;

    private int stage;

    private float attackTimer;
    private int attackIndex;

    public GameObject card;
    public GameObject[] cardSpawns;
    public GameObject poTer;
    public List<GameObject> poTerSpawns;
    
    public GameObject boom;

    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
        _maxHealth = health;

        healthSlider.value = health;
        healthSliderVis.value = health;
        
        SpawnKey();
        Search();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                attackTimer += Time.deltaTime;
                if (attackTimer >= 9)
                {
                    if (attackIndex >= 2)
                    {
                        attackIndex = 0;
                        Search();
                        break;
                    }
                    Attack();
                    attackIndex++;
                    attackTimer = 0;
                }
                break;
            case 1:
                attackTimer += Time.deltaTime;
                if (attackTimer >= 5)
                {
                    // if (attackIndex >= 3)
                    // {
                    //     
                    //     stage = 0;
                    //     attackIndex = 0;
                    //     break;
                    // }
                    Search();
                    attackTimer = 0;
                }
                break;
        }
    }

    public void Hurt()
    {
        health -= 33;
        healthSlider.value = health;
        StartCoroutine(AnimateSlider(healthSliderVis, healthSliderVis.value, healthSliderVis.value - 33f, 1));

        stage++;

        if (stage > 1)
        {
            stage = 0;
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("Credits");
        }
        
        SpawnKey();
    }

    public void Attack()
    {
        print("Attacking");
        anim.SetTrigger("PlayAttack");
    }

    public void Search()
    {
        print("Searching");
        //transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        StartCoroutine(LerpRotation(Quaternion.Euler(0, Random.Range(0f, 360f), 0), 1f));
        stage=0;
    }

    public void SpawnKey()
    {
        int rng = Random.Range(0, cardSpawns.Length);
        Instantiate(card, cardSpawns[rng].transform.position, cardSpawns[rng].transform.rotation);
        Instantiate(boom, cardSpawns[rng].transform.position, Quaternion.identity);
        
        int rng2 = Random.Range(0, poTerSpawns.Count);
        Instantiate(poTer, poTerSpawns[rng2].transform.position, poTerSpawns[rng2].transform.rotation);
        Instantiate(boom, poTerSpawns[rng2].transform.position, Quaternion.identity);
        poTerSpawns.Remove(poTerSpawns[rng2]);
    }
    
    
    
    
    
    
    
    
    //AI CODE GARBAGE - too lazy :(

    private IEnumerator AnimateSlider(Slider slider, float startValue, float endValue, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            slider.value = Mathf.Lerp(startValue, endValue, t);
            yield return null;
        }

        slider.value = endValue;
    }
    private IEnumerator LerpRotation(Quaternion target, float duration)
    {
        Quaternion start = transform.rotation;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(start, target, t / duration);
            yield return null;
        }
        transform.rotation = target;
    }
}