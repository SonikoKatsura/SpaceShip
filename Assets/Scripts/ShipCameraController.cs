using UnityEngine;
using UnityEngine.InputSystem;

public class ShipCameraController : MonoBehaviour
{
    // Referencia al objetivo que debe seguir la c�mara
    public Transform target;

    // Ajustes de distancia entre el objetivo y la c�mara (offset)
    public Vector3 offset;

    private float maxRotationAngle = 90f; // M�ximo �ngulo de rotaci�n permitido
    public float rotationSpeed = 50f; // Velocidad de rotaci�n de la c�mara
    public float followSpeed = 5f;

    private Quaternion originalRotation; // Rotaci�n original de la c�mara relativa al objetivo

    // Referencia al InputAction de los controles (c�mara)
    [SerializeField] InputActionReference cameraAction;


    private void Update()
    {
        Vector2 rotationDirection = cameraAction.action.ReadValue<Vector2>();

        // Calcular la nueva posici�n y rotaci�n de la c�mara
        Vector3 newPosition = transform.position + transform.forward * rotationDirection.y * followSpeed * Time.deltaTime;
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x - rotationDirection.y * rotationSpeed * Time.deltaTime,
                                                  transform.rotation.eulerAngles.y + rotationDirection.x * rotationSpeed * Time.deltaTime,
                                                  transform.rotation.eulerAngles.z);

        // Aplicar interpolaci�n suave para el movimiento de la c�mara
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }
}