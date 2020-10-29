using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : Character
{

    // Update is called once per frame
    void Update()
    {
        MovePath();
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(GameHandler.WorldToCell(Input.mousePosition) - GameHandler.originCell);
            CalculatePathTo(GameHandler.WorldToCell(Input.mousePosition));
        }
    }
}
