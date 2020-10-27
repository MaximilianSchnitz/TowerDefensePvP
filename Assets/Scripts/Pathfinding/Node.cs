using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Node
{

    public int X { get; }
    public int Y { get; }

    public Vector2 Position { get { return new Vector2(X, Y); } }

    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; } }

    public Node Parent { get; set; }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

}
