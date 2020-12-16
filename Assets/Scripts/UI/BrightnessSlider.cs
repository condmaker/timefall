using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    private float gammaCorrection;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        gammaCorrection = slider.value;

        RenderSettings.ambientLight = 
            new Color(gammaCorrection, gammaCorrection, gammaCorrection, 1.0f);
    }

    
}