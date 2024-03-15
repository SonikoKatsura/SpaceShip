using UnityEngine;

// ----------------------------------------------------------
// SCRIPT para que la c�mara siga constantemente al objetivo
// mediante un movimiento de interpolaci�n suave.
// ----------------------------------------------------------

public class CameraFollow : MonoBehaviour {

    // Referencia al objetivo que debe seguir la c�mara
    public Transform target;

    // Suavizado del movimiento de la c�mara para seguir al objetivo
    public float smoothSpeed = 0.125f;

    // Ajustes de distancia entre el objetivo y la c�mara (offset)
    public Vector3 offset;

    // M�todo que se ejecuta en cada frame del juego
    void Update() {

        // Se calcula la posici�n hacia la que se desea llegar
        Vector3 desiredPosition = target.position + offset;

        // Se aplica un Lerp para que la c�mara se desplace poco a poco (interpolaci�n)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Se asigna la nueva posici�n interpolada
        transform.position = smoothedPosition;

        // Se le pide a la c�mara que mire hacia el objetivo
        transform.LookAt(target);
    }

}