using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField]
    private ObjectData itemData;
    
    public ObjectData GetData { get => itemData; set { itemData = value; } }

    public void Start()
    {
        //Just here so teh script can be disabled
    }
}
