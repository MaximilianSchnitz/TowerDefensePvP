using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBase : MonoBehaviour
{
    public Sprite health0;
    public Sprite health1;
    public Sprite health2;
    public Sprite health3;
    public Sprite health4;
    public Sprite health5;
    public Sprite health6;

    Base baseHealth;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = transform.GetComponent<MonoBehaviour>() as Base;
        spriteRenderer = transform.Find("Health Bar").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baseHealth.health < baseHealth.maxHealth / 7)
            spriteRenderer.sprite = health6;
        else if (baseHealth.health < baseHealth.maxHealth / 7 * 2)
            spriteRenderer.sprite = health5;
        else if (baseHealth.health < baseHealth.maxHealth / 7 * 3)
            spriteRenderer.sprite = health4;
        else if (baseHealth.health < baseHealth.maxHealth / 7 * 4)
            spriteRenderer.sprite = health3;
        else if (baseHealth.health < baseHealth.maxHealth / 7 * 5)
            spriteRenderer.sprite = health2;
        else if (baseHealth.health < baseHealth.maxHealth / 7 * 6)
            spriteRenderer.sprite = health1;
        else if (baseHealth.health <= baseHealth.maxHealth)
            spriteRenderer.sprite = health0;

        if (baseHealth.health <= 0)
            Destroy(baseHealth.gameObject);
    }
}
