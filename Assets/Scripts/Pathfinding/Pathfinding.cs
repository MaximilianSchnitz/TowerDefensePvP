using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pathfinding
{
    Node[,] grid; // Map
    List<Node> openNodes; // 
    List<Node> closedNodes; // Nodes ignoriert werden sollen

    public Vector2 startLocation;
    public Vector2 targetLocation;

    Pathfinding Instance { get; } = new Pathfinding();

    private const int STRAIGHT_COST = 10; // Kosten für eine Bewegung nach Links, Recht, Oben oder Unten
    private const int DIAGONAL_COST = 14; // Kosten für eine diagonale Bewegung. Resultiert aus sqrt(1^2 + 1^2), was grob die Strecke von A nach C in einem rechtwinkligen
    //Dreieck mit Seitenlängen a = 1 und c = 1. Am Ende mal 10 genommen um mit int arbeiten zu können.

    public void Initialise(int width, int height)
    {
        grid = new Node[width, height];

        // Bei jeder Koordinate ein Node erzeugen
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                grid[x, y] = new Node(x, y);
    }

    public List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        var startNode = grid[startX, startY];
        var endNode = grid[endX, endY];

        openNodes = new List<Node> { startNode };
        closedNodes = new List<Node>();

        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                var node = grid[x, y];
                node.GCost = int.MaxValue;
                node.Parent = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);

        while(openNodes.Count > 0)
        {
            var currentNode = GetLowestFCostNode(openNodes);
            if (currentNode == endNode)
                return CalculatePath(endNode);

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            foreach(var neighbourNode in GetNeighbours(currentNode))
            {
                if (closedNodes.Contains(neighbourNode))
                    continue;

                int gCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(gCost < neighbourNode.GCost)
                {
                    neighbourNode.Parent = currentNode;
                    neighbourNode.GCost = gCost;
                    neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);

                    if (!openNodes.Contains(neighbourNode))
                        openNodes.Add(neighbourNode);
                }
            }
        }

        //Kein Pfad gefunden
        return null;
    }

    private List<Node> GetNeighbours(Node node)
    {
        var neighbours = new List<Node>();

        int x = node.X;
        int y = node.Y;

        //Links
        if (x - 1 >= 0)
        {
            neighbours.Add(grid[x - 1, y]);
            //Links Unten
            if (y - 1 >= 0)
                neighbours.Add(grid[x - 1, y - 1]);
            //Links Oben
            if (y + 1 < grid.GetLength(1))
                neighbours.Add(grid[x - 1, y + 1]);
        }
        //Rechts
        if (x + 1 < grid.GetLength(0))
        {
            neighbours.Add(grid[x + 1, y]);
            //Rechts Unten
            if (y - 1 >= 0)
                neighbours.Add(grid[x + 1, y - 1]);
            //Rechts Oben
            if (y + 1 < grid.GetLength(1))
                neighbours.Add(grid[x + 1, y + 1]);
        }
        //Unten
        if (y - 1 >= 0)
            neighbours.Add(grid[x, y - 1]);
        //Oben
        if (y + 1 < grid.GetLength(1))
            neighbours.Add(grid[x, y + 1]);

        return neighbours;
    }

    private List<Node> CalculatePath(Node endNode)
    {
        var path = new List<Node>();
        path.Add(endNode);
        var currentNode = endNode;
        while(currentNode.Parent != null)
        {
            path.Add(currentNode.Parent);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(Node a, Node b)
    {
        int difX = Mathf.Abs(a.X - b.X); // Delta x
        int difY = Mathf.Abs(a.Y - b.Y); // Delta y
        int timesStraight = Mathf.Abs(difX - difY); //Anzahl der "geraden"(links, rechts, oben, unten) Bewegungen. Sollen vermieden werden so viel es geht
        return Mathf.Min(difX, difY) * DIAGONAL_COST + timesStraight * STRAIGHT_COST; //Gesamte H Kosten
    }

    private Node GetLowestFCostNode(List<Node> nodes)
    {
        var lowest = nodes[0];
        foreach (var node in nodes)
            if (node.FCost < lowest.FCost)
                lowest = node;
        return lowest;
    }


}
