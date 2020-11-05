using System;
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
    [SerializeField]
    Tilemap subPathTileMap;

    private GameObject[] buttonGameObjs;
    private List<Button> buttons;
    private List<Vector2Int> occupiedTiles;

    private int selectedButton = -1;

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
                if(pathTileMap.GetTile(new Vector3Int(x, y, 0)) != null || subPathTileMap.GetTile(new Vector3Int(x, y, 0)) != null)
                    pathTiles.Add(new Vector2Int(x, y));

        //Button events zuweisen
        buttonGameObjs = GameObject.FindGameObjectsWithTag("Button");
        buttons = new List<Button>();

        Debug.Log(buttonGameObjs.Length);

        for(int i = 0; i < buttonGameObjs.Length; i++)
            buttons.Add(buttonGameObjs[i].GetComponent<MonoBehaviour>() as Button);

        GetByIndicator(0).buttonClicked += ButtonClicked0;

        //Tower Positionen
        occupiedTiles = new List<Vector2Int>();
        var towers = GameObject.FindGameObjectsWithTag("Tower");
    }

    private List<Vector2Int> GetOccupiedTilesFromTower(GameObject tower)
    {
        return GetOccupiedTilesFromTower(tower.GetComponent<MonoBehaviour>() as Tower);
    }

    private List<Vector2Int> GetOccupiedTilesFromTower(Tower tower)
    {
        var occTiles = new List<Vector2Int>();
        var towerPos = new Vector2Int(Mathf.FloorToInt(tower.transform.position.x), Mathf.FloorToInt(tower.transform.position.y));
        for(int x = towerPos.x + tower.bottomLeft.x; x <= towerPos.x + tower.topRight.x; x++)
        {
            for(int y = towerPos.y  + tower.bottomLeft.y; y <= towerPos.y + tower.topRight.y; y++)
            {
                occTiles.Add(new Vector2Int(x, y));
            }
        }
        return occTiles;
    }

    private void ButtonClicked0(Button sender)
    {
        selectedButton = 0;
    }

    private Button GetByIndicator(int indicator)
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].indicator == indicator)
                return buttons[i];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = WorldToCell(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && !occupiedTiles.Contains(mousePos) && !pathTiles.Contains(mousePos))
        {
            switch(selectedButton)
            {
                case 0:
                    var tower = Instantiate(Resources.Load("TestTower"), new Vector3(mousePos.x + .5f, mousePos.y + .5f, 0), Quaternion.identity);
                    selectedButton = -1;
                    occupiedTiles.AddRange(GetOccupiedTilesFromTower((GameObject) tower));
                    break;
                default:
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach(var b in buttons)
            {
                if (b.CheckPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                    b.Click();
            }
        }



        var cellPos = WorldToCell(Input.mousePosition);
        var gameObjPos = new Vector2(cellPos.x + 0.5f, cellPos.y + 0.5f);
        selectedField.transform.position = gameObjPos;


    }

    public static Vector2 CellToWorld(Vector2 cell)
    {
        var vec3 = new Vector3Int((int)cell.x, (int)cell.y, 1);
        return Camera.main.WorldToScreenPoint(grid.CellToWorld(vec3));
    }

    public static Vector2Int WorldToCell(Vector2 pos)
    {
        var vec3 = new Vector3Int((int)pos.x, (int)pos.y, 1);
        var cell = grid.WorldToCell(Camera.main.ScreenToWorldPoint(vec3));
        return new Vector2Int(cell.x, cell.y);
    }

}
