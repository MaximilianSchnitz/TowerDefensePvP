using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{

    public float radius;
    public int segments = 50;

    private LineRenderer line;

    private void Start()
    {
        line = transform.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        CreatePoints();
    }

    private void Update()
    {
        if (GameHandler.WorldToCell(Input.mousePosition) == new Vector2((int) transform.position.x, (int) transform.position.y))
            for (int i = 0; i < segments + 1; i++)
                line.SetPosition(i, new Vector3(line.GetPosition(i).x, line.GetPosition(i).y, 0));
        else
            for (int i = 0; i < segments + 1; i++)
                line.SetPosition(i, new Vector3(line.GetPosition(i).x, line.GetPosition(i).y, 5));




    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 0;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.startColor = Color.black;
            line.endColor = Color.black;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

}
