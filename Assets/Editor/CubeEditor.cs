using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Cube)), CanEditMultipleObjects]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  // Draw default UI

        serializedObject.Update();

        // Get the size propery from the cube script
        var size = serializedObject.FindProperty("size");
        serializedObject.ApplyModifiedProperties();

        // Create a helpbox warning if the cube size is invalid
        if (size.floatValue < 1f)
        {
            EditorGUILayout.HelpBox("Cube size cannot be smaller than 1", MessageType.Warning);
        }

        if (size.floatValue > 2f)
        {
            EditorGUILayout.HelpBox("Cube size cannot be larger than 2", MessageType.Warning);
        }

        //EditorGUILayout.BeginHorizontal();  // Have the buttons next to each other in the editor

        using (new EditorGUILayout.HorizontalScope())   // Place the buttons next to each other in the editor using a horizontal scope
        {
            // Create button for selecting all cubes in the scene
            if (GUILayout.Button("Select all cubes"))
            {
                var allCube = GameObject.FindObjectsOfType<Cube>();
                var allCubeGameObjects = allCube.Select(cube => cube.gameObject).ToArray();
                Selection.objects = allCubeGameObjects;
            }

            // Create button for clearing the selection
            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[0];
            }
        }

        //EditorGUILayout.EndHorizontal();

        // Add color to the button
        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        // Create button for enabling/disabling cubes, with the button being larger than normal
        if (GUILayout.Button("Disable/Enable all cubes", GUILayout.Height(40)))
        {    
            foreach (var cube in GameObject.FindObjectsOfType<Cube>(true))
            {              
                cube.gameObject.SetActive(!cube.gameObject.activeSelf);
                
                // Unsure if we want to clear out of inspector or not, but this is the code to do it
                //Selection.objects = new Object[0];
            }
        }
        GUI.backgroundColor = cachedColor;  // Change the button color back to default (doesn't work for some reason)
    }
}
