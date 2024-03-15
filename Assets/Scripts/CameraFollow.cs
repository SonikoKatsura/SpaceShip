using UnityEngine;

// ----------------------------------------------------------
// SCRIPT para que la cámara siga constantemente al objetivo
// mediante un movimiento de interpolación suave.
// ----------------------------------------------------------

public class CameraFollow : MonoBehaviour {

    // Referencia al objetivo que debe seguir la cámara
    public Transform target;

    // Suavizado del movimiento de la cámara para seguir al objetivo
    public float smoothSpeed = 0.125f;

    // Ajustes de distancia entre el objetivo y la cámara (offset)
    public Vector3 offset;

    // Método que se ejecuta en cada frame del juego
    void Update() {

        // Se calcula la posición hacia la que se desea llegar
        Vector3 desiredPosition = target.position + offset;

        // Se aplica un Lerp para que la cámara se desplace poco a poco (interpolación)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Se asigna la nueva posición interpolada
        transform.position = smoothedPosition;

        // Se le pide a la cámara que mire hacia el objetivo
        transform.LookAt(target);
    }

}