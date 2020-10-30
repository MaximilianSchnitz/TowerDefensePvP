using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestChar : Character
{
    [SerializeField]
    GameObject enemyBase;

    // Update is called once per frame
    void Update()
    {
        if(enemyBase != null)
            Attack(enemyBase);
        MovePath();
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(GameHandler.WorldToCell(Input.mousePosition) - GameHandler.originCell);
            CalculatePathTo(GameHandler.WorldToCell(Input.mousePosition));
        }

        if (Input.GetKeyDown(KeyCode.Space))
            health--;

    }
}
