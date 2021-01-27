using UnityEngine;

/// <summary>
/// Class responsible for storing an Objectdata 
/// </summary>
public abstract class DataHolder : MonoBehaviour
{
    /// <summary>
    /// Instance of an ObjectData to be stored
    /// </summary>
    [SerializeField]
    protected ObjectData itemData;

    /// <summary>
    /// Method responsible for retrieving the stored Data
    /// </summary>
    /// <param name="equipedItem">The item equipped by the player</param>
    /// <returns>The stored ObjectData</returns>
    public abstract ObjectData GetData(ItemData equipedItem = null);

    /// <summary>
    /// Method is called before the first fram of the update
    /// </summary>
    private void Start() { /*Empty to activate enabling*/ }

    /// <summary>
    /// Method responsible for setting the stored ObjectData
    /// </summary>
    /// <param name="equipedItem">The item equipped by the player</param>
    public virtual void SetData(ObjectData equipedItem)
    {
        itemData = equipedItem;
    }

    /// <summary>
    /// Method responsible for destroying the ObjectData
    /// </summary>
    internal abstract void DestroyObject();



}
