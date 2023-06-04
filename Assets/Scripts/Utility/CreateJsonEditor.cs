using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(CreateJson))]
public class CreateJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateJson script = (CreateJson)target;

        if (GUILayout.Button("Genera JSON"))
        {
            script.CreateJsonFile();
        }

        if (GUILayout.Button("Popola JSON"))
        {
            script.PopulateEnemySpawner();
        }

    }
}

#endif