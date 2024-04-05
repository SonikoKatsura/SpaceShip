using UnityEngine;

public class Menu : MonoBehaviour {

    public void StartGame()
    {
        if(CheckEasterEgg())
        {
            SceneController.instance.LoadScene("EasterEggScene");
        }
        else
        {
            SceneController.instance.LoadScene("GameScene");
        }
    }
    public void GoToSettings() {
        SceneController.instance.LoadScene("GeneralSettingsScene");
    }

    public void GoToRanking() {
        SceneController.instance.LoadScene("RankingScene");
    }
    public void GoToMenu() {
        SceneController.instance.LoadScene("MenuScene");
    }

    public void ReloadScene(string scene)
    {
        SceneController.instance.LoadScene(scene);
    }

    public bool CheckEasterEgg()
    {
        int savedSong = PlayerPrefs.GetInt("BackgroundMusicSelected");

        return savedSong == 4;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE_WIN
                Application.Quit();
        #endif
    }
}
