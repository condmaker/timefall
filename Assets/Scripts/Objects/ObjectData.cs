using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DataAsset", menuName = "DataAssets/Objects")]
public class ObjectData : ScriptableObject
{
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private string     description;
    [SerializeField]
    private int        iD;
    public Sprite      inventoryImage;

}
