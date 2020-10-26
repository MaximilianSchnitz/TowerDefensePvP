using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObj;

    private Grid grid;

    private void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
        gameObj = Instantiate(gameObj);
    }

    private void Update()
    {
        var cellPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        var gameObjPos = new Vector2(cellPos.x + 0.5f, cellPos.y + 0.5f);
        gameObj.transform.position = gameObjPos;
    }

}
