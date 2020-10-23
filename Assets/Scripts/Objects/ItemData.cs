using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Object Data/item Data")]
public class ItemData : ObjectData
{
    [SerializeField]
    private Sprite uiObjectSprite;
    public Sprite UIobjectSprite { get => uiObjectSprite; }
}
