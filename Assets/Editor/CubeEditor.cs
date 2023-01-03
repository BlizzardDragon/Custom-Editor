using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Cube cube = target as Cube;

        // Colors.
        GUILayout.Label("Color");
        EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            cube.Color = EditorGUILayout.ColorField(cube.Color);
            if (EditorGUI.EndChangeCheck())
            {
                cube.SetColor();
            }
            if (GUILayout.Button("Random Color"))
            {
                cube.SetRandomColor();
            }
        EditorGUILayout.EndHorizontal();

        // Scale.
        EditorGUILayout.LabelField("Scale");
        EditorGUI.BeginChangeCheck();
        cube.Scale = EditorGUILayout.Slider(cube.Scale, 0.1f, 1);
        if (EditorGUI.EndChangeCheck())
        {
            cube.SetScale();
        }
        
        // Random Scale.
        GUILayout.Label($"Random range scale: from ({cube.MinScale}) to ({cube.MaxScale})");
        EditorGUILayout.MinMaxSlider(ref cube.MinScale, ref cube.MaxScale, 0.1f, 1f);
        if (GUILayout.Button("Random Scale"))
        {
            cube.SetRandomScale();
        }
    }
}

