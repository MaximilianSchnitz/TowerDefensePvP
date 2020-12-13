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

    private GameObject normalSelectedField;

    public static Vector2Int originCell;
    public static Vector2Int mapCellSize = new Vector2Int(50, 30);

    public static List<Vector2Int> pathTiles;

    [SerializeField]
    Tilemap pathTileMap;

    private GameObject[] buttonGameObjs;
    private List<Button> buttons;
    private List<Vector2Int> occupiedTiles;

    private int selectedButton = -1;

    private Currency currency;

    // Start is called before the first frame update
    void Start()
    {
        currency = GetComponent<Currency>();

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

        //Button events zuweisen
        buttonGameObjs = GameObject.FindGameObjectsWithTag("Button");
        buttons = new List<Button>();

        for(int i = 0; i < buttonGameObjs.Length; i++)
            buttons.Add(buttonGameObjs[i].GetComponent<MonoBehaviour>() as Button);

        GetByIndicator(0).buttonClicked += ButtonClicked0;
        GetByIndicator(1).buttonClicked += ButtonClicked1;
        GetByIndicator(2).buttonClicked += ButtonClicked2;
        GetByIndicator(3).buttonClicked += ButtonClicked3;
        GetByIndicator(4).buttonClicked += ButtonClicked4;

        //Tower Positionen
        occupiedTiles = new List<Vector2Int>();
        var towers = GameObject.FindGameObjectsWithTag("Tower");
    }

    private Vector2Int GetOccupiedTilesFromTower(GameObject tower)
    {
        return GetOccupiedTilesFromTower(tower.GetComponent<MonoBehaviour>() as Turret);
    }



    private Vector2Int GetOccupiedTilesFromTower(Turret tower)
    {
        return new Vector2Int(Mathf.FloorToInt(transform.position.x), (int) Mathf.FloorToInt(transform.position.y));
    }

    private void ButtonClicked0(Button sender)
    {
        selectedButton = 0;
        SetSelectedField(GetTowerFromIndex(selectedButton), rotation);
    }

    private void ButtonClicked1(Button sender)
    {
        selectedButton = 1;
        SetSelectedField(GetTowerFromIndex(selectedButton), rotation);
    }

    private void ButtonClicked2(Button sender)
    {
        selectedButton = 2;
        SetSelectedField(GetTowerFromIndex(selectedButton), rotation);
    }

    private void ButtonClicked3(Button sender)
    {
        selectedButton = 3;
        SetSelectedField(GetTowerFromIndex(selectedButton), rotation);
    }

    private void ButtonClicked4(Button sender)
    {
        selectedButton = 4;
        SetSelectedField(GetTowerFromIndex(selectedButton), rotation);
    }

    private void SetSelectedField(GameObject gameObject, int rot)
    {
        Destroy(selectedField);
        var rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, rot);
        selectedField = Instantiate(GetTowerFromIndex(selectedButton), Vector3.zero, rotation);
    }

    private GameObject GetTowerFromIndex(int i)
    {
        switch(i)
        {
            case -1:
                return Resources.Load("Selected Field") as GameObject;
            case 0:
                return Resources.Load("Canon") as GameObject;
            case 1:
                return Resources.Load("Crossbow") as GameObject;
            case 2:
                return Resources.Load("Mortar") as GameObject;
            case 3:
                return Resources.Load("Trap") as GameObject;
            case 4:
                return Resources.Load("GoldMine") as GameObject;
            default:
                return null;
        }
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

    int rotation = 0;

    // Update is called once per frame
    void Update()
    {
        var mousePos = WorldToCell(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !occupiedTiles.Contains(mousePos) && (!pathTiles.Contains(mousePos) || selectedButton == 3))
        {
            foreach (var s in occupiedTiles)
                Debug.Log(s);
            if(selectedButton >= 0 && selectedButton <= 4)
            {
                var tower = GetTowerFromIndex(selectedButton);
                var price = (tower.GetComponent<MonoBehaviour>() as Turret).price;
                var rotatable = (tower.GetComponent<MonoBehaviour>() as Turret).rotatable;

                var rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, this.rotation);

                if (price <= currency.currency)
                {
                    currency.currency -= price;

                    GameObject turret;

                    if(rotatable)
                        turret = Instantiate(tower, new Vector3(mousePos.x + .5f, mousePos.y + .5f, 0), rotation);
                    else
                        turret = Instantiate(tower, new Vector3(mousePos.x + .5f, mousePos.y + .5f, 0), Quaternion.identity);

                    var obj = turret.GetComponent<Turret>();
                    if (selectedButton == 1)
                        obj.rotation = this.rotation;
                    else
                        obj.rotation = 360 - this.rotation;
                    obj.isPlaced = true;

                    occupiedTiles.Add(new Vector2Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y)));
                }
            }


            selectedButton = -1;
            SetSelectedField(normalSelectedField, 0);
        }

        if(Input.GetMouseButtonDown(1) && selectedButton != -1)
        {
            selectedButton = -1;
            SetSelectedField(normalSelectedField, 0);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            this.rotation += 90;
            if (this.rotation >= 360)
                this.rotation = 0;

            var rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, this.rotation);
            selectedField.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach(var b in buttons)
            {
                if (b.CheckPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    b.Click();
                }
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
