using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedDataHolder : DataHolder {

    private IList <DataHolder> holderList;
    private int Count => holderList.Count;

    private void Awake()
    {
        holderList = new List<DataHolder>();
    }

    public override ObjectData GetData(ItemData equipedItem = null)
    {
        if (equipedItem != null || holderList.Count < 1)
            return itemData;
        else return holderList[Count - 1].GetData(equipedItem);
    }

    public void AddData(DataHolder dH)
    {
        holderList.Add(dH);
    }

    internal override void DestroyObject()
    {
        holderList[Count - 1].DestroyObject();
        holderList.RemoveAt(Count - 1);
    }

}
