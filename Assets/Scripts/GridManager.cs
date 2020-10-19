using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField]
    private int rows = 10;
    [SerializeField]
    private int cols = 10;
    [SerializeField]
    private float tileSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateGrid()
    {
        var refTile = new GameObject();

        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                var tile = Instantiate(refTile, transform);

                float posX = col * tileSize;
                float posY = -row * tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }

        Destroy(refTile);
    }


}
