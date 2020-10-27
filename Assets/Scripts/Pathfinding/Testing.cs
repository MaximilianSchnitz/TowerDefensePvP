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
        aStar.Initialise(30, 30);

        var startLocation = new Vector2(0, 0);
        var endLocation = new Vector2(10, 20);

        Debug.Log("Start: " + startLocation);
        Debug.Log("Target: " + endLocation);

        path = aStar.FindPath(startLocation, endLocation);

        for(int i = 0; i < path.Count; i++)
            Debug.Log($"{i}: ({path[i].X}|{path[i].Y})");
    }

    // Update is called once per fixed frame
    int counter = 61;
    int i = 0;
    void FixedUpdate()
    {
        return;
        var currentNode = path[i];
        var nodePos = Camera.main.WorldToScreenPoint(grid.CellToWorld(new Vector3Int(currentNode.X, currentNode.Y, 1))); 
        transform.position = Vector2.MoveTowards(transform.position, nodePos, 1);
        if (transform.position == nodePos)
            if (i < path.Count)
                i++;

        if (counter <= 0)
            counter = 61;
        counter--;
    }
}
