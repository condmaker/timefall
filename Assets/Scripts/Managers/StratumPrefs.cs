using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StratumPrefs : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private int fCheck;

    [SerializeField]
    private Texture2D medievalSprite;
    [SerializeField]
    private Texture2D futuristicSprite;

    private void Awake()
    {
        // V Debug pra quando querermos colocar isto de volta a 0
        //PlayerPrefs.SetInt("F_Check", 0);

        if (fCheck >= 1)
            PlayerPrefs.SetInt("F_Check", 1);

        if (fCheck == 1)
        {
            CursorLoad(futuristicSprite);
        }
        else
        {
            CursorLoad(medievalSprite);
        }

        // if (PlayerPrefs.GetInt("F_Check", 1) ...
    }

    private void CursorLoad(Texture2D tex) => 
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
}
