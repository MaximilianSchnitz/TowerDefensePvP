using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected Pathfinding pathFinding;
    protected List<Node> path;
    protected int currentNodeNo;

    protected Vector2 originCell; // Zelle in der Linken unterer Ecke des Bildschirms. Wird benötigt, wenn die Koordinaten der Welt in die der Pathfinding map umgewandelt werden sollen,
                                  // da diese eine Array ist und somit nur nicht negative Koordinaten haben kann, das Standart grid jedoch seinen Ursprung in der Mitte des Bildschrims hat.

    // Start is called before the first frame update
    private void Start()
    {
        originCell = GridManager.WorldToCell(Vector2.zero);

        pathFinding = new Pathfinding();
        pathFinding.Initialise(GridManager.gridCellSize.x, GridManager.gridCellSize.y);
    }

    // Update is called once per frame
    private void Update()
    {

    }
    
    //Solange es einen berechneten Pfad gibt bewegt sich der Charakter entlang dieses Pfades.
    //Gibt true zurück wenn das Zeil erreicht ist und false wenn nicht
    public bool MovePath()
    {
        if (path is null)
            return true;

        var nextNodePos = path[currentNodeNo].Position;
        var nextPos = new Vector2(nextNodePos.x + .5f, nextNodePos.y + .5f) + originCell;
        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if((Vector2) transform.position == nextPos)
        {
            if (currentNodeNo < path.Count - 1)
            {
                currentNodeNo++;
            }
            else
            {
                path = null;
                currentNodeNo = 0;
            }
        }
        return false;
    }


}
