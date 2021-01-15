using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class ObjectData : ScriptableObject
{
    [SerializeField]
    private InteractionType interactionType;
    public InteractionType  InteractionType => interactionType;

    [SerializeField]
    private string     description;
    [SerializeField]
    private short iD;
    public short ID => iD;

}


