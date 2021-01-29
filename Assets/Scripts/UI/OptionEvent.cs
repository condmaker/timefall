using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Class used for disabling player movement while the menu is open.
/// </summary>
public class OptionEvent : MonoBehaviour
{
    /// <summary>
    /// Event that triggers when option is enabled.
    /// </summary>
    public event Action OptionEnabled;

    /// <summary>
    /// Event that triggers when option is disabled. 
    /// </summary>
    public event Action OptionDisabled;

    private void OnEnable() => OnOptionEnabled();
    private void OnDisable() => OnOptionDisabled();
    private void OnOptionEnabled() => OptionEnabled();
    private void OnOptionDisabled() => OptionDisabled();
}
