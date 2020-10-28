using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed; //Bewegungsgeschwindigkeit

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
    
    //Berechnet den Pfad zu angegebenen GameObject und setzt pfad Variable
    protected void CalculatePathTo(GameObject gameObject)
    {
        path = pathFinding.FindPath((Vector2) transform.position - originCell, (Vector2) gameObject.transform.position - originCell);
    }

    //Solange es einen berechneten Pfad gibt bewegt sich der Charakter entlang dieses Pfades.
    //Gibt true zurück wenn das Zeil erreicht ist und false wenn nicht
    protected bool MovePath()
    {
        if (path is null) //Wenn es keinen Pfad gibt, nicht bewegen
            return true;

        var nextNodePos = path[currentNodeNo].Position; //Position des nächsten Nodes in Form der Pathfinding map
        var nextPos = new Vector2(nextNodePos.x + .5f, nextNodePos.y + .5f) + originCell; //Pathfinding map Koordinaten in grid Koordinaten (+0.5f, da ansonsten die Koordinaten zwischen den Feldern liegen
                                                                                          // und nicht wie gewollt auf den Feldern)
        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime); //Mit der gewählten Geschwindigkeit zum nächsten Node bewegen

        if((Vector2) transform.position == nextPos) // Wenn nächstes Node erreicht wurde entweden nächste Node neu wählen, falls es eines gibt, ansonsten path = null um zeigen, dass sich nicht bewegt werden soll
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
