using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField]
    GameObject target;

    [SerializeField]
    Grid grid;
    // Start is called before the first frame update
    Pathfinding aStar;
    List<Node> path = new List<Node>();
    void Start()
    {
        aStar = new Pathfinding();
        aStar.Initialise(33, 15);


    }

    // Update is called once per fixed frame
    int counter = 61;
    int i = 0;
    void FixedUpdate()
    {
        
        var currentNode = path[i];
        var currentPos = Camera.main.WorldToScreenPoint(grid.CellToWorld(new Vector3Int((int) transform.position.x, (int) transform.position.y, 1)));
        transform.position = Vector2.MoveTowards(currentPos, currentNode.Position, 1);
        if (currentPos == currentNode.Position)
            if (i < path.Count)
                i++;

        if (counter <= 0)
            counter = 61;
        counter--;
    }

    private Vector2 WorldToCell(Vector2 pos)
    {
        var vec3 = new Vector3Int((int) pos.x, (int) pos.y, 1);
        return Camera.main.WorldToScreenPoint(grid.CellToWorld());
    }

}
