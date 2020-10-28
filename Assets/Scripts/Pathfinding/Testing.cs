using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{

    [SerializeField]
    GameObject target;
    [SerializeField]
    Vector2 mapCellSize;

    private Vector2 originCell;

    Pathfinding aStar;
    List<Node> path;

    // Start is called before the first frame update
    void Start()
    {
        originCell = GridManager.WorldToCell(Vector2.zero);

        aStar = new Pathfinding();
        aStar.Initialise((int) mapCellSize.x, (int) mapCellSize.y);
    }
    int i = 0;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(GridManager.WorldToCell(Input.mousePosition) - originCell);
            var cellPos = new Vector2(transform.position.x, transform.position.y) - originCell;
            var targetCell = GridManager.WorldToCell(Input.mousePosition) - originCell;
            path = aStar.FindPath(cellPos, targetCell);
            i = 0;
            Debug.Log(targetCell + originCell);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(GridManager.WorldToCell(Input.mousePosition) - originCell);
            var targetCell = GridManager.WorldToCell(Input.mousePosition) - originCell;
            aStar.grid[(int) targetCell.x, (int) targetCell.y].IsWalkable = !aStar.grid[(int)targetCell.x, (int)targetCell.y].IsWalkable;
            var newWall = Instantiate(target);
            var renderer = newWall.GetComponent<SpriteRenderer>();
            renderer.color = Color.black;
            newWall.transform.position = new Vector2(targetCell.x + 0.5f, targetCell.y + 0.5f) + originCell;
        }
    }

    // Update is called once per fixed frame
    int counter = 61;
    void FixedUpdate()
    {

        if (path is null)
            return;

        var nextCellPos = path[i].Position;
        var nextPos = new Vector2(nextCellPos.x + 0.5f, nextCellPos.y + 0.5f);
        //Debug.Log(nextPos);

        transform.position = Vector2.MoveTowards(transform.position, nextPos + originCell, 3 * Time.fixedDeltaTime);
        if ((Vector2)transform.position == nextPos + originCell)
            i++;

        //Debug.Log(transform.position);

        //if (path is null)
        //    return;

        //var nextNode = path[i];
        //var nextPos = nextNode.Position + originCell;
        //var currentPos = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        //transform.position = Vector2.MoveTowards(new Vector2(currentPos.x + .5f, currentPos.y + .5f), new Vector2(nextPos.x + .5f, nextPos.y + .5f), 2 * Time.fixedDeltaTime);
        //if (currentPos == nextPos)
        //{
        //    if (i < path.Count)
        //        i++;
        //    else
        //        path = null;
        //}

        //Debug.Log("Current Pos: " + currentPos);
        //Debug.Log("Next Pos: " + nextPos);

    }

}
