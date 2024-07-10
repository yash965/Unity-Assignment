using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This stores the data of the obstacles in a boolean array and it is made as a scriptable object.

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[] obstacleGrid = new bool[100];
}
