
using UnityEngine;

/// <summary>
/// Class responsible for controlling the game's keybindings.
/// </summary>
[CreateAssetMenu(fileName = "KeyBinding", menuName = 
    "ScriptableObjects/KeyBindings")]
public class KeyBindings: ScriptableObject
{
    /// <summary>
    /// Property that defines the Key used to strafe left.
    /// </summary>
    public KeyCode StrafeLeft { get; private set; }

    /// <summary>
    /// Property that defines the Key used to strafe right.
    /// </summary>
    public KeyCode StrafeRight { get; private set; }

    /// <summary>
    /// Property that defines the Key used to rotate left.
    /// </summary>
    public KeyCode RotateLeft { get; private set; }

    /// <summary>
    /// Property that defines the Key used to rotate right.
    /// </summary>
    public KeyCode RotateRight { get; private set; }

    /// <summary>
    /// Method that runs at game start to load the stored keybindings.
    /// </summary>
    private void Awake()
    {
        UpdateStrafe();
    }

    /// <summary>
    /// Updates the rotation and strafe keybindings depending on player 
    /// preference.
    /// </summary>
    public void UpdateStrafe()
    {
        //Strafe Controls
        int strafeSetup = PlayerPrefs.GetInt("strafeKey");
        switch (strafeSetup)
        {
            case 0:
                StrafeLeft = KeyCode.A;
                StrafeRight = KeyCode.D;
                RotateLeft = KeyCode.Q;
                RotateRight = KeyCode.E;
                break;
            case 1:
                StrafeLeft = KeyCode.Q;
                StrafeRight = KeyCode.E;
                RotateLeft = KeyCode.A;
                RotateRight = KeyCode.D;
                break;
        }
    }

}
