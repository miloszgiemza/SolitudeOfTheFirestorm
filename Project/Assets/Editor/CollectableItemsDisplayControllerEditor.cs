using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(CollectableItemsDisplayController))]
public class CollectableItemsDisplayControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CollectableItemsDisplayController collectableItemsDisplayController = (CollectableItemsDisplayController)target;

        if(GUILayout.Button("Refresh"))
        {
            collectableItemsDisplayController.Display(Map.Instance.MapData);
        }
    }
}
