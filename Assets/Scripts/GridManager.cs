using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObj;

    private static Grid grid;

    public static Vector2Int gridCellSize = new Vector2Int(33, 16);


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

    public static Vector2 CellToWorld(Vector2 cell)
    {
        var vec3 = new Vector3Int((int)cell.x, (int)cell.y, 1);
        return Camera.main.WorldToScreenPoint(grid.CellToWorld(vec3));
    }

    public static Vector2 WorldToCell(Vector2 pos)
    {
        var vec3 = new Vector3Int((int)pos.x, (int)pos.y, 1);
        var cell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(vec3));
        return new Vector2(cell.x, cell.y);
    }

}
