using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow
{
    string example = "Insert text here...";

    [MenuItem("Window/Dialogue Tree")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TestWindow>("Dialogue Tree");
    }
    void OnGUI() 
    {
        //windowCode
        GUILayout.Box("yo");
        GUILayout.Label("Generate dialogue text and organise the order.", EditorStyles.boldLabel);
        example = GUILayout.TextArea(example);
        if(GUILayout.Button("Create Dialogue Field"))
        {
            
        }
        
    }
}
