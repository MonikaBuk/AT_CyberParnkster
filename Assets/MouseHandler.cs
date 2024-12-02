using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    private void OnEnable()
    {
        EnableMouse();
    }

    private void OnDisable()
    {
        DisableMouse();
    }

    private void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
        Debug.Log("Mouse enabled!");
    }

    private void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
        Debug.Log("Mouse disabled!");
    }
}
