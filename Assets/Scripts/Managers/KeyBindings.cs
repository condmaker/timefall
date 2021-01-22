
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBinding", menuName = "ScriptableObjects/KeyBindings")]
public class KeyBindings: ScriptableObject
{
    public KeyCode StrafeLeft { get; private set; }
    public KeyCode StrafeRight { get; private set; }

    public KeyCode MoveLeft { get; private set; }
    public KeyCode MoveRight { get; private set; }

    private void Awake()
    {
        UpdateStrafe();
    }

    public void UpdateStrafe()
    {
        //Strafe Controls
        int strafeSetup = PlayerPrefs.GetInt("strafeKey");
        switch (strafeSetup)
        {
            case 0:
                StrafeLeft = KeyCode.A;
                StrafeRight = KeyCode.D;
                MoveLeft = KeyCode.Q;
                MoveRight = KeyCode.E;
                break;
            case 1:
                StrafeLeft = KeyCode.Q;
                StrafeRight = KeyCode.E;
                MoveLeft = KeyCode.A;
                MoveRight = KeyCode.D;
                break;
        }
    }

}
