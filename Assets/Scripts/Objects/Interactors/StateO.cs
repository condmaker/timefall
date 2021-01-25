using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateO
{
    [SerializeField]
    private ObjectStateHandler osh;
    [SerializeField]
    private short state;
    [SerializeField]
    private bool any;

    public ObjectStateHandler Osh => osh;
    public short State => state;
    public bool Any => any;
}