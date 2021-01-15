using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDataHolder : DataHolder
{
    public override ObjectData GetData(ItemData equipedItem = null)
    {
        return itemData;
    }

    internal override void DestroyObject()
    {
        Destroy(gameObject);
    }

}
