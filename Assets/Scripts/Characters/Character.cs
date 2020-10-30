using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public int price;
    [SerializeField]
    public float speed; //Bewegungsgeschwindigkeit
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float damage;

    protected Pathfinding pathFinding;
    protected List<Node> path;
    protected int currentNodeNo;

    protected Vector2 originCell; // Zelle in der Linken unterer Ecke des Bildschirms. Wird benötigt, wenn die Koordinaten der Welt in die der Pathfinding map umgewandelt werden sollen,
                                  // da diese eine Array ist und somit nur nicht negative Koordinaten haben kann, das Standart grid jedoch seinen Ursprung in der Mitte des Bildschrims hat.

    [NonSerialized]
    public float health;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        originCell = GameHandler.WorldToCell(Vector2.zero);

        pathFinding = new Pathfinding();
        pathFinding.Initialise(GameHandler.mapCellSize.x, GameHandler.mapCellSize.y);

        foreach (var tile in GameHandler.pathTiles)
            pathFinding.grid[tile.x - GameHandler.originCell.x, tile.y - GameHandler.originCell.y].IsWalkable = true;

        health = maxHealth;
    }

    protected virtual void Attack(GameObject enemyBase)
    {
        if (Vector2.Distance(transform.position, enemyBase.transform.position) > 1)
            return;

        ((Base) enemyBase.GetComponent<MonoBehaviour>()).health -= damage;
        Debug.Log("Damage done!");
        Instantiate(transform.gameObject, Vector3.zero, Quaternion.identity);
        Destroy(transform.gameObject);
    }
    
    //Berechnet den Pfad zu angegebenen GameObject und setzt pfad Variable
    protected void CalculatePathTo(Vector2 pos)
    {
        path = pathFinding.FindPath((Vector2) transform.position - originCell, pos - originCell);
        currentNodeNo = path != null && path.Count > 1 ? 1 : 0; //Soll mit Bewegung zum nächsten Node anfangen um bei schnellem ändern des Pfad die Bewegung flüssiger zu machen, falls der Pfad lang genug ist
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
                currentNodeNo = 0;
                path = null;
            }
        }
        return false;
    }


}
