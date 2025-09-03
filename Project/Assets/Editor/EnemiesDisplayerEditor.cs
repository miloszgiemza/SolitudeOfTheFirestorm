using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(EnemiesDisplayControler))]
public class EnemiesDisplayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemiesDisplayControler gameplayObjectsDisplayController = (EnemiesDisplayControler)target;

        if(GUILayout.Button("Refresh"))
        {
            gameplayObjectsDisplayController.Display(EnemiesController.Instance.EnemiesOnMap);
        }
    }
}
