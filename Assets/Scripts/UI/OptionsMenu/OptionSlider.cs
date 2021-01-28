using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that represents a Option Slider Component
/// </summary>
[RequireComponent(typeof(Slider))]
public class OptionSlider : MonoBehaviour
{
    /// <summary>
    /// Name of the component
    /// This name will be used when saving to the PlayerPrefs
    /// </summary>
    [SerializeField]
    private string sliderName = default;

    /// <summary>
    /// Property that defines the component's name
    /// </summary>
    public string SliderName => sliderName;

    /// <summary>
    /// Slider component of the OptionSlider
    /// </summary>
    public Slider Slider { get; private set; }

    /// <summary>
    /// Text to display the slider value on
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI text = default;

    /// <summary>
    /// Event triggered when the slider changes values
    /// </summary>
    public Action<float, string> onValueChange;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    private void Awake()
    {
        Slider = GetComponent<Slider>();
        Slider.onValueChanged.AddListener(UpdateValue);
    }

    /// <summary>
    /// Method responsible for updating the value of the OptionSlider
    /// </summary>
    /// <param name="value">Value of the slider</param>
    public void UpdateValue(float value)
    {
        text.text = $"{sliderName}: {value}%";
        PlayerPrefs.SetInt(sliderName, (int)value);
        Slider.value = value;
        onValueChange.Invoke(value, sliderName);
    }

}
