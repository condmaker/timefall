using UnityEngine;

/// <summary>
/// Class responsible for managing the Control tab of the Options Menu
/// </summary>
public class ControlOptions : MonoBehaviour
{
    /// <summary>
    /// Option selector to select what keys the strafe uses
    /// </summary>
    [SerializeField]
    private OptionSelector strafeSelector = default;

    /// <summary>
    /// The key bindings for the player
    /// </summary>
    [SerializeField]
    private KeyBindings playerBinds = null;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    private void Awake()
    {
        strafeSelector.onSelect += playerBinds.UpdateStrafe;
    }

}
