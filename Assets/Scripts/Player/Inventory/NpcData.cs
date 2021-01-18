﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Object Data/NPC Data")]

public class NpcData : ObjectData
{
    [SerializeField]
    private DialogueScript dialogue;
    public DialogueScript Dialogue { get => dialogue; }
}
