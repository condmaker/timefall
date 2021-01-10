using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    private float gammaCorrection;
    private Slider slider;
    [SerializeField]
    private Light PlayerLight;
    private float minIntensity;
    private float maxIntensity;
    private float minRange;
    private float maxRange;

    private void Start()
    {
        minIntensity = 3;
        maxIntensity = 3;
        minRange = 10;
        maxRange = 30;

        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        gammaCorrection = slider.value;
        PlayerLight.intensity = 
            (minIntensity + slider.value * (maxIntensity - minIntensity));
        PlayerLight.range =
            (minRange + slider.value * (maxRange - minRange));
        //RenderSettings.ambientLight = 
        //    new Color(gammaCorrection, gammaCorrection, gammaCorrection, 1.0f);
    }

    
}