﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Object Data/Item Data")]
public class ItemData : ObjectData
{
    [SerializeField]
    private Sprite uiObjectSprite;
    public Sprite UIobjectSprite { get => uiObjectSprite; }

    [SerializeField]
    private Mesh mesh;
    public Mesh Mesh { get => mesh; }
}