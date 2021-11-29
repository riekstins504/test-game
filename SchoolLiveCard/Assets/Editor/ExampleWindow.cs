using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ExampleWindow : EditorWindow
{
    private Color m_color;
    
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }
    
    void OnGUI()
    {
        GUILayout.Label("Hello world!", EditorStyles.boldLabel);    
        EditorGUILayout.LabelField("haha");
        EditorGUILayout.ColorField("Color", m_color);
    }
}
