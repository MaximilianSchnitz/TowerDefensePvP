using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Node
{
    //Koordinaten
    public int X { get; }
    public int Y { get; }

    public Vector2 Position { get { return new Vector2(X, Y); } }

    //Setzt ob Node bei Berechnung des Pfad berücksichtigt werden soll oder nicht
    public bool IsWalkable { get; set; }

    //Kosten
    public int GCost { get; set; } //Kosten die benötigt sind um von Startfeld auf aktuelles Feld zu kommen
    public int HCost { get; set; } //Vorraussichtliche Kosten um von aktuellem Feld ans Zeil zu kommen (Es wird angenommen, dass der direkte Weg genommen wird)
    public int FCost { get { return GCost + HCost; } } //G- und H-Kosten addiert

    public Node Parent { get; set; } //Node von dem gekommen wird

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

}
