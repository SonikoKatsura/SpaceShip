using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] TMP_Text countText;
    [SerializeField] int MAX_GEMS = 10;
    int count = 0;
    private GameManager gameManager;

    void Start()
    {
        countText.text = $"GEMAS: {MAX_GEMS - count}";
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlaySFX("GetGem");
        AddCount();
        Destroy(other.gameObject);
        gameManager.RemoveEnemy(other.transform);
    }

    void AddCount()
    {
        count++;
        countText.text = $"GEMAS: {MAX_GEMS - count}";
        if (count == MAX_GEMS)
        {
            PlayerPrefs.SetInt("score", (int)GameObject.Find("HUD").GetComponent<Timer>().remainingTime);
            SceneController.instance.LoadScene("GameOverScene");
        }
    }
}