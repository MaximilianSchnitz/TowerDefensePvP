using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameHandler : MonoBehaviour
{

    public static Grid grid;
    private GameObject selectedField;

    public static Vector2Int originCell;
    public static Vector2Int mapCellSize = new Vector2Int(34, 16);

    public static List<Vector2Int> pathTiles;

    [SerializeField]
    Tilemap pathTileMap;
    

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();

        var tempCell = WorldToCell(Vector2.zero);
        originCell = new Vector2Int((int) tempCell.x, (int) tempCell.y);

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
        var cellPos = WorldToCell(Input.mousePosition);
        var gameObjPos = new Vector2(cellPos.x + 0.5f, cellPos.y + 0.5f);
        selectedField.transform.position = gameObjPos;
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
