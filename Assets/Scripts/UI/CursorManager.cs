using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoSingleton<CursorManager>
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    void Awake()
    {
        ChangeCursor(cursor);
        //Cursor.lockState = CursorLockMode.Confined;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}
