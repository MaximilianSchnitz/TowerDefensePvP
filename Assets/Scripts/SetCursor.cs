using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D basicCursor;
    [SerializeField]
    private bool customCursor = false;
    [SerializeField]
    private int offsetX = 0;
    [SerializeField]
    private int offsetY = 0;

    // Start is called before the first frame update
    void Start()
    {
        var cursorOffset = new Vector2(offsetX, offsetY);
        Cursor.SetCursor(basicCursor, cursorOffset, CursorMode.Auto);   
    }
}
