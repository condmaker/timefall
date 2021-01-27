using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "ScriptableObjects/Object Data/Item Data")]
public class ItemData : ObjectData
{
    [SerializeField]
    private Sprite uiObjectSprite = null;
    public Sprite UIobjectSprite { get => uiObjectSprite; }

    [SerializeField]
    private Mesh mesh;
    public Mesh Mesh { get => mesh; }
}
