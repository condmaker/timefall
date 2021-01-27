using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(Iteration))]
public class IteratorEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, 
        GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position,
            GUIUtility.GetControlID(FocusType.Passive),
            label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect typeRect = new Rect(
            position.x,
            position.y + 5,
            position.width * 0.6f, position.height - 7);

        Rect labelRect = new Rect(
            position.x + position.width * 0.6f + 5,
            position.y + 5,
            position.width * 0.3f, position.height - 9);

        Rect useRect = new Rect(
            position.x + position.width - 20, 
            position.y + 5,
            position.width * 0.2f, position.height - 9);


        SerializedProperty typeProp = 
            property.FindPropertyRelative("iteration");
        SerializedProperty useProp =
            property.FindPropertyRelative("specific");
        SerializedProperty stateProp =
            property.FindPropertyRelative("specificState");

        if (useProp.boolValue)
            EditorGUI.PropertyField(typeRect, stateProp, GUIContent.none);
        else
            EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

        EditorGUI.LabelField(labelRect, "Specific Number");
        EditorGUI.PropertyField(useRect, useProp, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 27;
    }
}
