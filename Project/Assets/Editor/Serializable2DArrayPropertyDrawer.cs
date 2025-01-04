using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

[CustomPropertyDrawer(typeof(Serializable2DArray<>))]
public class Serializable2DArrayPropertyDrawer : PropertyDrawer
{
    float fieldHeight = 50;
    float fieldWidth = 50;
    float horizontalMargin = 10f;
    float verticalMargin = 10f;

    private float totalHeight = 20f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //pobranie tablicy z klasy Serializable2DArray
        SerializedProperty rowsArray = property.FindPropertyRelative("rows");

        Rect newPos = position;
        newPos.y += 70;

        for (int i = 0; i < rowsArray.arraySize; i++)
        {
            SerializedProperty row = rowsArray.GetArrayElementAtIndex(i).FindPropertyRelative("row");

            for (int j = 0; j < row.arraySize; j++)
            {
                Rect newFieldRect = new Rect(new Vector2(newPos.x, newPos.y), new Vector2(fieldWidth, fieldHeight));
                EditorGUI.PropertyField(newFieldRect, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPos.x += fieldWidth + horizontalMargin;
            }

            newPos.x = position.x;
            newPos.y += fieldHeight + verticalMargin;
            totalHeight = newPos.y;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + totalHeight;
    }
}
