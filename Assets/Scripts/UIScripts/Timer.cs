using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour {
    public float remainingTime = 0f;
    private bool timerRunning = true;

    void Update() {
        if (timerRunning) {
            remainingTime += Time.deltaTime;

            string minutes = (Mathf.Floor(Mathf.Round(remainingTime) / 60)).ToString();
            string seconds = (Mathf.Round(remainingTime) % 60).ToString();

            if (minutes.Length == 1) { minutes = "0" + minutes; }
            if (seconds.Length == 1) { seconds = "0" + seconds; }

            GameObject.Find("Timer").GetComponent<TMP_Text>().text = minutes + ":" + seconds;
        }
    }

    public void StopTimer() {
        timerRunning = false;
    }
}
