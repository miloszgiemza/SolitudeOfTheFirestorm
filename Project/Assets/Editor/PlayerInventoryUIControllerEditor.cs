using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(PlayerInventoryUIController))]
public class PlayerInventoryUIControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerInventoryUIController playerInventoryUIController = (PlayerInventoryUIController) target;

        if(GUILayout.Button("Refresh"))
        {
            playerInventoryUIController.UpdateInventory();
        }
    }
}
