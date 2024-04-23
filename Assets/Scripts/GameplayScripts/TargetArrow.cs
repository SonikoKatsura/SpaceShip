using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public Transform ship;
    public float rotationSpeed;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (gameManager.gems.Count == 0)
            return; // No hay objetivos, no hagas nada

        Transform closestTarget = GetClosestTarget(); // Obtener el objetivo más cercano
        if (closestTarget != null)
        {
            // Apuntar hacia el objetivo más cercano
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(closestTarget.position - ship.position), rotationSpeed * Time.deltaTime);
        }
    }

    Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform potentialTarget in gameManager.gems)
        {
            if (potentialTarget == null)
                continue;
            float distance = Vector3.Distance(ship.position, potentialTarget.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = potentialTarget;
            }
        }

        return closestTarget;
    }
}
