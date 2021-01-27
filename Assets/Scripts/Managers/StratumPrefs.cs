using UnityEngine;

/// <summary>
/// Class responsible for storing the information that the player
/// has reached the third stratum
/// </summary>
public class StratumPrefs : MonoBehaviour
{
    /// <summary>
    /// Variable that defines if the player has reached the third stratum
    /// </summary>
    [SerializeField]
    [Range(0, 1)]
    private int fCheck = 0;

    /// <summary>
    /// Cursor texture with medieval aesthetic 
    /// </summary>
    [SerializeField]
    private Texture2D medievalSprite = null;

    /// <summary>
    /// Cursor texture with futuristic aesthetic 
    /// </summary>
    [SerializeField]
    private Texture2D futuristicSprite = null;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    private void Awake()
    {
        // V Debug pra quando querermos colocar isto de volta a 0
        //PlayerPrefs.SetInt("F_Check", 0);

        if (fCheck >= 1)
            PlayerPrefs.SetInt("F_Check", 1);


        // if (PlayerPrefs.GetInt("F_Check", 1) ...
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        if (fCheck == 1)
        {
            CursorLoad(futuristicSprite);
        }
        else
        {
            CursorLoad(medievalSprite);
        }
    }

    /// <summary>
    /// Method responsible for switching the cursor texture
    /// </summary>
    /// <param name="tex">New cursor texture</param>
    private void CursorLoad(Texture2D tex) => 
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
}
