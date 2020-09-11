#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasicNodePath)), CanEditMultipleObjects]
public class CreateNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BasicNodePath nodePath = (BasicNodePath)target;
        if (GUILayout.Button("Create Node"))
        {
            nodePath.CreateNewNode();
        }

        if (GUILayout.Button("Create Objective"))
        {
            nodePath.CreateObjective();
        }
    }

}
#endif 