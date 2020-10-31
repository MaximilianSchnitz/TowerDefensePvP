using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameHandler : MonoBehaviour
{

    public static Grid grid;
    private GameObject selectedField;

    public static Vector2Int originCell;
    public static Vector2Int mapCellSize = new Vector2Int(36, 20);

    public static List<Vector2Int> pathTiles;

    [SerializeField]
    Tilemap pathTileMap;
    

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();

        var temp = WorldToCell(Vector2.zero);
        originCell = new Vector2Int((int) temp.x, (int) temp.y);

        //Feldmarkierung laden
        selectedField = (GameObject) Instantiate(Resources.Load("Selected Field"), Vector3.zero, Quaternion.identity);


        //Begehbaren Pfad bestimmen
        pathTiles = new List<Vector2Int>();

        for(int x = originCell.x; x < mapCellSize.x + originCell.x; x++)
            for(int y = originCell.y; y < mapCellSize.y + originCell.y; y++)
                if(pathTileMap.GetTile(new Vector3Int(x, y, 0)) != null)
                    pathTiles.Add(new Vector2Int(x, y));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(WorldToCell(Input.mousePosition));
            Debug.Log(WorldToCell(Input.mousePosition) - originCell);
        }

        var cellPos = WorldToCell(Input.mousePosition);
        var gameObjPos = new Vector2(cellPos.x + 0.5f, cellPos.y + 0.5f);
        selectedField.transform.position = gameObjPos;

        if(Input.GetMouseButtonDown(1))
        {
            var mousePos = WorldToCell(Input.mousePosition);
            if(!pathTiles.Contains(new Vector2Int((int) mousePos.x, (int) mousePos.y)))
            {
                Instantiate(Resources.Load("TestTower"), new Vector3(mousePos.x + .5f, mousePos.y + .5f, 0), Quaternion.identity);
            }
        }
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
