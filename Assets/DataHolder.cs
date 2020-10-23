using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{

    [SerializeField]
    private ObjectData itemData;
    
    public Object GetData()
    {
        return itemData;
    }
}
