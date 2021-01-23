using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OptionEvent : MonoBehaviour
{
    public event Action OptionEnabled;
    public event Action OptionDisabled;

    private void OnEnable() => OnOptionEnabled();
    private void OnDisable() => OnOptionDisabled();
    private void OnOptionEnabled() => OptionEnabled();
    private void OnOptionDisabled() => OptionDisabled();
}
