using UnityEngine;

public abstract class DataHolder : MonoBehaviour
{
    [SerializeField]
    protected ObjectData itemData;

    public abstract ObjectData GetData(ItemData equipedItem = null);

    /// <summary>
    /// Start for enable property
    /// </summary>
    private void Start()
    { }
    public virtual void SetData(ObjectData equipedItem)
    {
        itemData = equipedItem;
    }

    internal abstract void DestroyObject();



}
