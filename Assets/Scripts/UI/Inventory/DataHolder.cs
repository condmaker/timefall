using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField]
    private ObjectData itemData;
    
    public ObjectData GetData => itemData;
}
