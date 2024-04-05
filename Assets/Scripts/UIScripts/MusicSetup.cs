using UnityEngine;

public class MusicSetup : MonoBehaviour {
    void Start() {
        int savedSong = PlayerPrefs.GetInt("BackgroundMusicSelected", 0);
        float savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.72f);
        AudioManager.instance.PlayMusic(savedSong.ToString());
        AudioManager.instance.ChangeVolume(savedVolume);
    }
}
