using System.Collections;
using System.Collections.Generic;
using Unity;
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

    /// <summary>
    /// Brightness slider object
    /// </summary>
    [SerializeField]
    private Slider brightnessSlider = default;

    /// <summary>
    /// Small map object
    /// </summary>
    [SerializeField]
    private GameObject mapSmall = default;

    /// <summary>
    /// Big map object
    /// </summary>
    [SerializeField]
    private GameObject mapBig = default;

    /// <summary>
    /// Array of all possible screen resolutions.
    /// </summary>
    private Resolution[] resolutions;

    /// <summary>
    /// Variable that controls whether it's the first slider update during 
    /// canvas creation, or not.
    /// </summary>
    private bool? firstSliderUpdate;

    /// <summary>
    /// Player Light object.
    /// </summary>
    [SerializeField]
    private Light PlayerLight = default;

    /// <summary>
    /// Variable that controls the current brightness.
    /// </summary>
    private float currentBrightness;

    /// <summary>
    /// Minimum PlayerLight Intensity.
    /// </summary>
    private float minIntensity;

    /// <summary>
    /// Maximum PlayerLight Intensity.
    /// </summary>
    private float maxIntensity;

    /// <summary>
    /// Minimum PlayerLight Range.
    /// </summary>
    private float minRange;

    /// <summary>
    /// Maximum PlayerLight Range.
    /// </summary>
    private float maxRange;

    /// <summary>
    /// Called when the object is created. Sets the dropdown menus with all 
    /// and the current selected options.
    /// </summary>
    void Awake()
    {
        if (PlayerLight != null)
            InitLightValues();
        if (brightnessSlider != null)
            SetBrightnessSlider();
        SetResolutionsDropdown();
        SetFullscreenDropdown();
    }

    /// <summary>
    /// Method responsible for initializing the Light's values.
    /// </summary>
    private void InitLightValues()
    {
        minIntensity = 3;
        maxIntensity = 4;
        minRange = 10;
        maxRange = 30;
    }

    /// <summary>
    /// Séts the current brightness value into the brightness slider.
    /// </summary>
    private void SetBrightnessSlider()
    {
        currentBrightness = PlayerPrefs.GetFloat("Brightness",  0.5f);
        brightnessSlider.value = currentBrightness;
    }

    /// <summary>
    /// Set the current full screen mode into the drop down menu.
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

            if(r.width == PlayerPrefs.GetInt("ResW", 1280) &&
                r.height == PlayerPrefs.GetInt("ResH", 720))
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

        PlayerPrefs.SetInt("ResW", res.width);
        PlayerPrefs.SetInt("ResH", res.height);

        Screen.SetResolution(PlayerPrefs.GetInt("ResW"),
            PlayerPrefs.GetInt("ResH"), Screen.fullScreenMode);
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

    /// <summary>
    /// Sets the brightness slider value into the current brightness level 
    /// and player pref variable.
    /// </summary>
    /// <param name="brightnessLevel">The selected value from the brightness 
    /// slider.</param>
    public void SetBrightness(float brightnessLevel)
    {
        currentBrightness = brightnessLevel;
        UpdateLight();
        PlayerPrefs.SetFloat("Brightness", currentBrightness);
    }

    /// <summary>
    /// Updates the PlayerLight with the current brightness value.
    /// </summary>
    private void UpdateLight()
    {
        PlayerLight.intensity =
            (minIntensity + currentBrightness * (maxIntensity - minIntensity));
        PlayerLight.range =
            (minRange + currentBrightness * (maxRange - minRange));
    }

    /// <summary>
    /// Method responsible for toggling the menu between on and off states.
    /// </summary>
    public void ToggleMap()
    {
        mapSmall.SetActive(!mapSmall.activeSelf);
        mapBig.SetActive(!mapBig.activeSelf);
    }
}
