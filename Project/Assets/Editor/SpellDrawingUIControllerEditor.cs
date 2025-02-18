using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(SpellDrawingUIController))]
public class SpellDrawingUIControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpellDrawingUIController spellDrawingUIControllerEditor = (SpellDrawingUIController)target;

        if (GUILayout.Button("Refresh"))
        {
            spellDrawingUIControllerEditor.Refresh();
        }
    }
}
