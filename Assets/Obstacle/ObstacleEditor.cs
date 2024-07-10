using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    // This script is used to make a custom editor for our ObstacleData scriptable component in which we make a toggleable 
    // bool array of elements. This is made horizontal with flipped x and y axis while displaying the layout of the boolean 
    // array.


    ObstacleData obstacleData;

    private void OnEnable()
    {
        obstacleData = (ObstacleData)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Obstacle Grid Editor", EditorStyles.boldLabel);

        for (int y = 0; y < 10; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                int index = x + y * 10;
                obstacleData.obstacleGrid[index] = GUILayout.Toggle(obstacleData.obstacleGrid[index], "");
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
        }

    }
}
