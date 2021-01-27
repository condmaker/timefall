/// <summary>
/// Data holder with only one item stored
/// </summary>
public class SimpleDataHolder : DataHolder
{

    /// <summary>
    /// Method responsible for retrieving the stored Data
    /// </summary>
    /// <param name="equipedItem">The item equipped by the player</param>
    /// <returns>The stored ObjectData</returns>
    public override ObjectData GetData(ItemData equipedItem = null)
    {
        return itemData;
    }

    /// <summary>
    /// Method responsible for destroying the ObjectData
    /// </summary>
    internal override void DestroyObject()
    {
        Destroy(gameObject);
    }

}
