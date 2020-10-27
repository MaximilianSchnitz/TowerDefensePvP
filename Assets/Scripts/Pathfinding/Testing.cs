using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{

    [SerializeField]
    GameObject target;
    [SerializeField]
    Grid grid;
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
        aStar.Initialise(33, 15);
    }
    int i = 0;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(GridManager.WorldToCell(Input.mousePosition) - originCell);
            var cellPos = new Vector2(transform.position.x, transform.position.y);
            var targetCell = GridManager.WorldToCell(Input.mousePosition) - originCell;
            path = aStar.FindPath(cellPos, targetCell);
            i = 0;
            Debug.Log(targetCell + originCell);
        }
    }

    // Update is called once per fixed frame
    int counter = 61;
    void FixedUpdate()
    {

        if (path is null)
            return;

        var nextPos = path[i].Position;

        //Debug.Log(nextPos);

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(nextPos.x, nextPos.y), 3 * Time.fixedDeltaTime);
        if ((Vector2)transform.position == path[i].Position + originCell)
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

    private Vector2 CellInGrid(Vector2 cell)
    {
        return new Vector2(cell.x - originCell.x, cell.y - originCell.y);
    }

}
