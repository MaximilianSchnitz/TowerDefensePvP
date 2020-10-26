using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField]
    GameObject target;

    // Start is called before the first frame update
    Pathfinding aStar;
    List<Node> path = new List<Node>();
    void Start()
    {
        aStar = new Pathfinding();
        aStar.Initialise(30, 20);
        path = aStar.FindPath((int)transform.position.x, (int)transform.position.y, (int)target.transform.position.x, (int)target.transform.position.y);
    }

    // Update is called once per fixed frame
    int counter = 61;
    int i = 0;
    void FixedUpdate()
    {

        

        var currentNode = path[i];
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentNode.X, currentNode.Y), 1);
        if (transform.position == new Vector3(currentNode.X, currentNode.Y, transform.position.z))
            if (i < path.Count)
                i++;

        if (counter <= 0)
            counter = 61;
        counter--;
    }
}
