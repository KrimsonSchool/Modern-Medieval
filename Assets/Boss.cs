using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _maxHealth = health;

        healthSlider.value = health;
        healthSliderVis.value = health;
        
        SpawnKey();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                attackTimer += Time.deltaTime;
                if (attackTimer >= 5)
                {
                    Attack();
                    attackTimer = 0;
                }
                break;
            case 1:
                attackTimer += Time.deltaTime;
                if (attackTimer >= 10)
                {
                    if (attackIndex >= 3)
                    {
                        SpawnKey();
                        stage++;
                        break;
                    }
                    Search();
                    attackTimer = 0;
                    attackIndex++;
                }
                break;
        }
    }

    public void Hurt()
    {
        health -= 25;
        healthSlider.value = health;
        StartCoroutine(AnimateSlider(healthSliderVis, healthSliderVis.value, healthSliderVis.value - 25f, 1));

        stage++;
    }

    public void Attack()
    {
        print("Attacking");
    }

    public void Search()
    {
        print("Searching");
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
}