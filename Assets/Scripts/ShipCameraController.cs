using UnityEngine;
using UnityEngine.InputSystem;

public class ShipCameraController : MonoBehaviour
{
    // Referencia al objetivo que debe seguir la cámara
    public Transform target;

    // Ajustes de distancia entre el objetivo y la cámara (offset)
    public Vector3 offset;

    private float maxRotationAngle = 90f; // Máximo ángulo de rotación permitido
    public float rotationSpeed = 50f; // Velocidad de rotación de la cámara
    public float followSpeed = 5f;

    private Quaternion originalRotation; // Rotación original de la cámara relativa al objetivo

    // Referencia al InputAction de los controles (cámara)
    [SerializeField] InputActionReference cameraAction;


    private void Update()
    {
        Vector2 rotationDirection = cameraAction.action.ReadValue<Vector2>();

        // Calcular la nueva posición y rotación de la cámara
        Vector3 newPosition = transform.position + transform.forward * rotationDirection.y * followSpeed * Time.deltaTime;
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x - rotationDirection.y * rotationSpeed * Time.deltaTime,
                                                  transform.rotation.eulerAngles.y + rotationDirection.x * rotationSpeed * Time.deltaTime,
                                                  transform.rotation.eulerAngles.z);

        // Aplicar interpolación suave para el movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }
}