using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotArea : MonoBehaviour
{

    // Get LayerMask in the Unity Inspector
    [SerializeField] private LayerMask slingShotArea;

    // Check if mouse within sling shot area, return a bool value
    // within : ở trong
    public bool IsWithinSlingShotArea() 
    {

        // Get position of mouse from screen coordinates to world coordinates
        // coordinates : tọa độ
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Check if the world position overlaps with the area defined by the slingShotArea LayerMask
        // overlap : chồng chéo
        // define : định nghĩa
        if (Physics2D.OverlapPoint(worldPosition, slingShotArea)) 
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }
}
