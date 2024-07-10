using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // This contains the info of the position of player. It contains the getter and setter method for the position info.


    public Vector2Int gridPosition;

    private void Start()
    {
        SetPosition(gridPosition.x, gridPosition.y);
    }

    public void SetPosition(int x, int z)
    {
        gridPosition = new Vector2Int(x, z);
        transform.position = new Vector3(x, 0.5f, z);
    }

    public Vector2Int GetPosition()
    {
        return gridPosition;
    }
}
