using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(EnemiesController))]
public class EnemiesControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemiesController enemiesController = (EnemiesController)target;

        if(GUILayout.Button("Clear Dead Enemies"))
        {
            enemiesController.ClearDeadEnemies();
        }
    }
}
