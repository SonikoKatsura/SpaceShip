using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour {

    [SerializeField] TMP_Text countText;
    [SerializeField] int MAX_GEMS = 10;
    int count = 0;

    void Start() {
        countText.text = $"GEMS LEFT: {MAX_GEMS - count}";
    }

    void OnTriggerEnter(Collider other) {
        AudioManager.instance.PlaySFX("GetGem");
        AddCount();
        Destroy(other.gameObject);
    }

    void AddCount() {
        count++;
        countText.text = $"GEMS LEFT: {MAX_GEMS - count}";
        if (count == MAX_GEMS) {
            PlayerPrefs.SetInt("score", (int) GameObject.Find("HUD").GetComponent<Timer>().remainingTime);
            SceneController.instance.LoadScene("GameOverScene");
        }
    }
}