using UnityEngine;
using UnityEngine.EventSystems;
using System.Text;

public class AudioOptions : MonoBehaviour
{
    [SerializeField]
    private OptionSlider masterSlider;
    [SerializeField]
    private OptionSlider[] audioSlider;

    [SerializeField]
    private SoundMng soundManager;

    private void Start()
    {

        masterSlider.onValueChange += UpdateMaster;
        int masterVal = PlayerPrefs.GetInt("Master Volume", 100);
        masterSlider.UpdateValue(masterVal);

        foreach (OptionSlider os in audioSlider)
        {
            os.onValueChange += UpdateRealVolume;
            int value = PlayerPrefs.GetInt(os.SliderName, 100);
            os.UpdateValue(value);
        }

        UpdateVolume();

    }

    
    public void UpdateMaster(float value, string sliderName)
    {
        UpdateVolume();

        foreach(OptionSlider os in audioSlider)
        {
            UpdateRealVolume(os.Slider.value, os.SliderName);
        }

        UpdateVolume();
    }

    public void UpdateRealVolume(float value, string sliderName)
    {
        
           
        StringBuilder realName = new StringBuilder(sliderName, 30);

        realName.Append(" Real");
        float masterVol = masterSlider.Slider.value / 100;
        int realVol = (int)(value * masterVol);

        PlayerPrefs.SetInt(realName.ToString(), realVol);

        UpdateVolume();
    }


    public void UpdateVolume()
    {
        soundManager.CurrentMusic.volume 
            = (float)((float)PlayerPrefs.GetInt("Music Volume Real") / 100);

        //Map decreasing volume. Max decrease is -20 for a smother volume change
        float sfxVal = ((float)(
            ((float)PlayerPrefs.GetInt("SFX Volume Real")) * 20) / 100)
        - 20;

        //If value is 0 completely mute audio mixer
        if (PlayerPrefs.GetInt("SFX Volume Real") == 0)
            sfxVal = -80;

        soundManager.Master.audioMixer.SetFloat("sfxVol", sfxVal);
            
           
    } 
}
