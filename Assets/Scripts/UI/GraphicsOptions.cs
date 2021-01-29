using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Class responsible for controlling graphic options.
/// </summary>
public class GraphicsOptions : MonoBehaviour
{
    /// <summary>
    /// Resolution Dropdown object.
    /// </summary>
    [SerializeField]
    private TMP_Dropdown resolutionDropdown = default;

    /// <summary>
    /// Fullscreen Dropdown object
    /// </summary>
    [SerializeField]
    private TMP_Dropdown fullScreenDropdown = default;

    [SerializeField]
    private Slider brightnessSlider = default;

    [SerializeField]
    private Light PlayerLight = default;

    private Resolution[] resolutions;

    /// <summary>
    /// Called when the object is created. Sets the dropdown menus with all 
    /// and the current selected options.
    /// </summary>
    void Awake()
    {
        SetResolutionsDropdown();
        SetFullscreenDropdown();
    }

    private void SetBrightnessSlider()
    {

    }

    /// <summary>
    /// Select the current full screen mode into the drop down menu.
    /// </summary>
    private void SetFullscreenDropdown()
    {
        int currentModeIndex = 0;
        FullScreenMode currentMode = Screen.fullScreenMode;
        switch (currentMode)
        {
            case FullScreenMode.FullScreenWindow:
                currentModeIndex = 0;
                break;
            
            case FullScreenMode.ExclusiveFullScreen:
                currentModeIndex = 1;
                break;
            
            case FullScreenMode.Windowed:
                currentModeIndex = 2;
                break;
        }

        fullScreenDropdown.value = currentModeIndex;
        fullScreenDropdown.RefreshShownValue();
    }

    /// <summary>
    /// This method loads all the possible 
    /// screen resolutions defined by Unity, and writes them into the 
    /// ResolutionDropdown menu.
    /// </summary>
    private void SetResolutionsDropdown()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions.Select(resolution => new Resolution 
            { width = resolution.width, 
            height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        foreach(Resolution r in resolutions)
        {
            string aux = r.width + " x " + r.height;
            resolutionOptions.Add(aux);

            if(r.width == Screen.currentResolution.width &&
                r.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = resolutionOptions.Count - 1;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Sets the selected resolution as current screen resolution.
    /// </summary>
    /// <param name="resolutionIndex">The selected resolution index in the 
    /// dropdown menu.</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    /// <summary>
    /// Sets the selected FullScreenMode as the current Full Screen Mode.
    /// </summary>
    /// <param name="FullScreenIndex">The selected full screen mode index 
    /// from the dropdown menu.</param>
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
