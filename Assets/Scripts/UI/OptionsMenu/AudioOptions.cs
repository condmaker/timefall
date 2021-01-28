using UnityEngine;
using System.Text;

/// <summary>
/// Class responsible for handling the audio options menu components
/// and behaviour
/// </summary>
public class AudioOptions : MonoBehaviour
{
    /// <summary>
    /// OptionSlider component that controls the master audio
    /// </summary>
    [SerializeField]
    private OptionSlider masterSlider = default;
    
    /// <summary>
    /// List of OptionSlider components that control specific audio preferences
    /// </summary>
    [SerializeField]
    private OptionSlider[] audioSlider = default;

    /// <summary>
    /// Manager responsible for playing the audio of the game
    /// </summary>
    [SerializeField]
    private SoundMng soundManager = default;

    /// <summary>
    /// Method called before the first frame of the Update
    /// </summary>
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
  
    /// <summary>
    /// Method responsible for updating the master volume 
    /// </summary>
    /// <param name="value">Value the slider was updated to</param>
    /// <param name="sliderName">Name of the slider that was updated</param>
    public void UpdateMaster(float value, string sliderName)
    {
        UpdateVolume();

        foreach(OptionSlider os in audioSlider)
        {
            UpdateRealVolume(os.Slider.value, os.SliderName);
        }

        UpdateVolume();
    }

    /// <summary>
    /// Method responsible for updating a specific audio with its real values 
    /// </summary>
    /// <param name="value">Value the slider was updated to</param>
    /// <param name="sliderName">Name of the slider that was updated</param>
    public void UpdateRealVolume(float value, string sliderName)
    {
                
        StringBuilder realName = new StringBuilder(sliderName, 30);

        realName.Append(" Real");
        float masterVol = masterSlider.Slider.value / 100;
        int realVol = (int)(value * masterVol);

        PlayerPrefs.SetInt(realName.ToString(), realVol);

        UpdateVolume();
    }

    /// <summary>
    /// Method responsible for updating all volume taking into account 
    /// the specific volumes and the master volume
    /// </summary>
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
