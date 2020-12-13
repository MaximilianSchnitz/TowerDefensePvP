using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{

    [SerializeField]
    public float maxHealth;

    [NonSerialized]
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(transform.gameObject);
            Environment.Exit(0);
            Application.Quit(0);
        }
    }

}