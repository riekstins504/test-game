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
        
        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate Color"))
            {
                cube.GenerateColor();
            }
            
            if (GUILayout.Button("Reset Color"))
            {
                cube.Reset();
            }
        GUILayout.EndHorizontal();

        cube.size = EditorGUILayout.Slider("Size",cube.size, .1f, 10f);
        cube.transform.localScale = Vector3.one * cube.size;
    }
}
