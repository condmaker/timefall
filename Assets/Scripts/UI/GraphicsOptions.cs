using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GraphicsOptions : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private int currentResolutionIndex;

    // Start is called before the first frame update
    void Awake()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution 
            { width = resolution.width, 
            height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        List<string> resos = new List<string>();

        foreach(Resolution r in resolutions)
        {
            string aux = r.width + " x " + r.height;
            resos.Add(aux);

            if(r.Equals(Screen.currentResolution))
            {
                currentResolutionIndex = resos.Count - 1;
            }
        }

        resolutionDropdown.AddOptions(resos);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    public void SetFullScreen(int FullScreenIndex)
    {
        FullScreenMode mode = Screen.fullScreenMode;
        switch (FullScreenIndex)
        {
            case 0:
                mode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                mode = FullScreenMode.Windowed;
                break;
        }
        Screen.fullScreenMode = mode;
    }
}
