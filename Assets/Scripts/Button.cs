using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ButtonClick(Button sender);

public class Button : MonoBehaviour
{
    public float width;
    public float height;

    public int indicator;

    private Vector2 bottomLeft;
    private Vector2 topRight;

    public event ButtonClick buttonClicked;

    private void Awake()
    {
        transform.tag = "Button";
    }

    private void Start()
    {
        bottomLeft = new Vector2(transform.position.x - width / 2, transform.position.y - height / 2);
        topRight = new Vector2(transform.position.x + width / 2, transform.position.y + height / 2);
    }

    public bool CheckPosition(Vector2 mousePos)
    {
        if (mousePos.x < topRight.x && mousePos.y < topRight.y && mousePos.x > bottomLeft.x && mousePos.y > bottomLeft.y)
            return true;
        return false;
    }

    public void Click()
    {
        OnButtonClick();
    }

    protected void OnButtonClick()
    {
        buttonClicked?.Invoke(this);
    }

}
