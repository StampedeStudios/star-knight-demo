using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateJson))]
public class CreateJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateJson script = (CreateJson)target;
        
        if (GUILayout.Button("Genera Json"))
        {
            script.CreateJsonFile();
        }
    }
}

