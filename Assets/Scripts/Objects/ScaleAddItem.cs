using UnityEngine;

/// <summary>
/// Class responsible for adding an item to the scale children
/// when the player interacts with the scale
/// </summary>
public class ScaleAddItem : MonoBehaviour
{
    /// <summary>
    /// Instance of the Interactor assigned to the scale
    /// </summary>
    private ItemInteractor iI;

    /// <summary>
    /// Instance of the data holder assigned to the scale
    /// </summary>
    private MixedDataHolder dH;

    /// <summary>
    /// Item to add to the scale
    /// </summary>
    [SerializeField]
    private GameObject itemPrefab = default;

    /// <summary>
    /// Method called before the first frame update
    /// </summary>
    void Start()
    {
        dH = gameObject.GetComponent<MixedDataHolder>();
        iI = gameObject.GetComponent<ItemInteractor>();
        iI.addData += AddItemToScale;
    }

    /// <summary>
    /// Method that gets an item Data, creates an object with the same data and
    /// adds it to the scale 
    /// </summary>
    /// <param name="iData">Data of the item to create</param>
    private void AddItemToScale(ItemData iData)
    {        
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform);
        item.GetComponent<MeshFilter>().mesh = iData.Mesh;
        DataHolder itemDH =  item.GetComponent<DataHolder>();
        itemDH.SetData(iData);
        dH.AddData(itemDH);
        
    }
}
