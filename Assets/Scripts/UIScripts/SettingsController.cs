using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    [SerializeField] Slider slider;
    [SerializeField] TMP_Dropdown selector;

    
    private float savedVolume;
    private int savedSong;
    private float selectedVolume;
    private int selectedSong;

    void Start() {
        savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        savedSong = PlayerPrefs.GetInt("BackgroundMusicSelected");
        selectedVolume = savedVolume;
        selectedSong = savedSong;
        LoadVolume(savedVolume);
        LoadSong(savedSong);
    }
    void LoadVolume(float volume) {
        slider.value = volume;
    }

    void LoadSong(int selection) {
        selector.value = selection;
    }


    public void ChangeVolume()
    {
        if (selectedVolume != slider.value)
        {
            selectedVolume = slider.value;
            AudioManager.instance.ChangeVolume(slider.value);
            PlayerPrefs.SetFloat("BackgroundMusicVolume", selectedVolume);
        }
    }


    public void ChangeSongSelected()
    {
        if (selectedSong != selector.value)
        {
            selectedSong = selector.value;
            AudioManager.instance.PlayMusic(selector.value.ToString());
            PlayerPrefs.SetInt("BackgroundMusicSelected", selector.value);
        }
    }


    public void SaveSettings() { 
        PlayerPrefs.Save();
    }
}