using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D basicCursor;

    // Start is called before the first frame update
    void Start()
    {
        var cursorOffset = Vector2.zero;
        //Cursor.SetCursor(basicCursor, cursorOffset, CursorMode.Auto);   
    }


    private void Update()
    {

    }

}
