using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> gems;

    public void RemoveEnemy(Transform gem)
    {
        gems.Remove(gem);
        // Verifica si la lista de enemigos está vacía
    }
    }
