using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EasterText"))
        {
            SceneController.instance.LoadScene("GameScene");
        }
    }
}
