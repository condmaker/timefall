using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAddItem : MonoBehaviour
{

    private ItemInteractor iI;
    private MixedDataHolder dH;

    [SerializeField]
    private GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        dH = gameObject.GetComponent<MixedDataHolder>();
        iI = gameObject.GetComponent<ItemInteractor>();
        iI.addData += AddItemToScale;
    }

    private void AddItemToScale(ItemData iData)
    {        
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform);
        item.GetComponent<MeshFilter>().mesh = iData.Mesh;
        DataHolder itemDH =  item.GetComponent<DataHolder>();
        itemDH.SetData(iData);
        dH.AddData(itemDH);
        
    }
}
