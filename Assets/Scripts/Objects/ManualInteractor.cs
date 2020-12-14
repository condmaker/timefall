using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ManualInteractor : MonoBehaviour, IInteractor
{

    [SerializeField]
    private short unlockerId;
    
    [SerializeField]
    private bool needsItem;
    
    //Eu acho q tem de ser public. 
    public event Action OnGoToFirst;
    public event Action OnGoToLast;
    public event Action OnGoToNext;

    public void Toggle(short? itemId)
    {
        if (unlockerId == itemId || !needsItem)
        {
            OnGoToNext?.Invoke();
        }
        else
            print("YOU. SHALL NOT. PASS");              
    }
}

//AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
/*
[CustomEditor(typeof(ManualInteractor))]
 public class ManualInteractorEditor : Editor
{

    private SerializedProperty needsItem;
    private SerializedProperty unlockerId;

    protected virtual void OnEnable()
    {
        needsItem = serializedObject.FindProperty("needsItem");
        unlockerId = serializedObject.FindProperty("unlockerId");
    }

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        needsItem. = GUILayout.Toggle(needsItem.boolValue, "Needs Item");
        if (needsItem.boolValue == true)
        {
            unlockerId.intValue = (short)EditorGUILayout.IntField("Item ID", unlockerId.intValue);
        }
    }
    
}
*/
