using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataHolder : MonoBehaviour
{
    [SerializeField]
    protected ObjectData itemData;

    public abstract ObjectData GetData(ItemData equipedItem = null);


    public virtual void SetData(ObjectData equipedItem)
    {
        itemData = equipedItem;
    }

    internal abstract void DestroyObject();



}
