using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OptionSlider : MonoBehaviour
{
    [SerializeField]
    private string sliderName;
    public string SliderName => sliderName;

    private Slider slider;
    public Slider Slider => slider;

    [SerializeField]
    private TextMeshProUGUI text;
    //public TextMeshProUGUI Text => text;

    public Action<float, string> onValueChange;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateValue);
    }

    public void UpdateValue(float value)
    {
        text.text = $"{sliderName}: {value}%";
        PlayerPrefs.SetInt(sliderName, (int)value);
        Slider.value = value;
        onValueChange.Invoke(value, sliderName);
    }

}
