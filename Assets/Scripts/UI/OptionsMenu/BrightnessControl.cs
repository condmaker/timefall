using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Method responsible for handling the brightness slider
/// </summary>
public class BrightnessControl : MonoBehaviour
{

    private float gammaCorrection;
    private Slider slider;

    [SerializeField]
    private Light PlayerLight = default;
    private float minIntensity;
    private float maxIntensity;
    private float minRange;
    private float maxRange;


    /// <summary>
    /// Method called before the first frame of the Update
    /// </summary>
    private void Start()
    {
        minIntensity = 3;
        maxIntensity = 3;
        minRange = 10;
        maxRange = 30;

        slider = GetComponent<Slider>();
    }


    /// <summary>
    /// Method called once per frame
    /// </summary>
    public void UpdateLight()
    {
        //gammaCorrection = slider.value;
        PlayerLight.intensity = 
            (minIntensity + slider.value * (maxIntensity - minIntensity));
        PlayerLight.range =
            (minRange + slider.value * (maxRange - minRange));
        RenderSettings.ambientLight = 
            new Color(gammaCorrection, gammaCorrection, gammaCorrection, 1.0f);
    }

    
}