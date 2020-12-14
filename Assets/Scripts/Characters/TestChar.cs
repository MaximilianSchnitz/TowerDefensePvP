using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestChar : Character
{
    private GameObject enemyBase;

    protected override void Start()
    {
        base.Start();

        enemyBase = GameObject.FindGameObjectWithTag("Base");
        CalculatePathTo(enemyBase);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyBase != null)
            Attack(enemyBase);
        MovePath();


    }
}
