using JetBrains.Annotations;
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
    private int        iD;
    public int ID => iD;

}


