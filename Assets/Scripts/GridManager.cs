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
        var cellPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x + 24, Input.mousePosition.y + 24)));
        gameObj.transform.position = grid.CellToWorld(cellPos);
    }

}
