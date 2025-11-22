using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Texture2D WhiteCursorTexture;
    public Texture2D BlueCursorTexture;
    public Texture2D BlackCursorTexture;
    public Texture2D RedCursorTexture; 
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(WhiteCursorTexture, hotSpot, cursorMode);
    }
    public void ChangeBrushColor(string color)
    {
        switch (color)
        {
            case "Red":
                Cursor.SetCursor(RedCursorTexture, hotSpot, cursorMode);
                break;
            case "Blue":
                Cursor.SetCursor(BlueCursorTexture, hotSpot, cursorMode);
                break;
            case "Black":
                Cursor.SetCursor(BlackCursorTexture, hotSpot, cursorMode);
                break;
            case "White":
                Cursor.SetCursor(WhiteCursorTexture, hotSpot, cursorMode);
                break;
        }
    }
}
