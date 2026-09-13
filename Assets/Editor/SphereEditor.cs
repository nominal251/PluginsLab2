using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Sphere)), CanEditMultipleObjects]
public class SphereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        // Get the size propery from the sphere script
        var size = serializedObject.FindProperty("radius");
        serializedObject.ApplyModifiedProperties();

        // Create a helpbox warning if the sphere size is invalid
        if (size.floatValue < 1f)
        {
            EditorGUILayout.HelpBox("Sphere size cannot be smaller than 1", MessageType.Warning);
        }

        if (size.floatValue > 2f)
        {
            EditorGUILayout.HelpBox("Sphere size cannot be larger than 2", MessageType.Warning);
        }

        //EditorGUILayout.BeginHorizontal();  // Have the buttons next to each other in the editor

        using (new EditorGUILayout.HorizontalScope())   // Place the buttons next to each other in the editor using a horizontal scope
        {
            // Create button for selecting all spheres in the scene
            if (GUILayout.Button("Select all spheres"))
            {
                var allSphere = GameObject.FindObjectsOfType<Sphere>();
                var allSphereGameObjects = allSphere.Select(sphere => sphere.gameObject).ToArray();
                Selection.objects = allSphereGameObjects;
            }

            // Create button for clearing the selection
            if (GUILayout.Button("Clear selection"))
            {
                Selection.objects = new Object[0];

                // Unsure if we want to clear out of inspector or not, but this is the code to do it
                //Selection.objects = new Object[0];
            }
        }

        //EditorGUILayout.EndHorizontal();

        // Add color to the button
        var cachedColor = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        // Create button for enabling/disabling spheres, with the button being larger than normal
        if (GUILayout.Button("Disable/Enable all spheres", GUILayout.Height(40)))
        {    
            foreach (var sphere in GameObject.FindObjectsOfType<Sphere>(true))
            {              
                sphere.gameObject.SetActive(!sphere.gameObject.activeSelf);               
            }
        }
        GUI.backgroundColor = cachedColor;  // Change the button color back to default (doesn't work for some reason)

    }
}
