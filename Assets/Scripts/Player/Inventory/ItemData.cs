using UnityEngine;

/// <summary>
/// Class responsible for storing the data of an Item
/// </summary>
[CreateAssetMenu(fileName = "Data Asset", menuName = "ScriptableObjects/Object Data/Item Data")]
public class ItemData : ObjectData
{
    /// <summary>
    /// Sprite of the item to be displayed in the UI
    /// </summary>
    [SerializeField]
    private Sprite uiObjectSprite = null;

    /// <summary>
    /// Property that defines the sprite of the item to be displayed in the UI
    /// </summary>
    public Sprite UIobjectSprite { get => uiObjectSprite; }

    /// <summary>
    /// Mesh of the item
    /// </summary>
    [SerializeField]
    private Mesh mesh;

    /// <summary>
    /// Property that defines the mesh of the item
    /// </summary>
    public Mesh Mesh { get => mesh; }
}
