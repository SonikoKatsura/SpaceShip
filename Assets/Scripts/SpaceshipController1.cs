using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// SCRIPT para realizar los movimientos de la nave espacial
// mediante el nuevo Input System de Unity
public class SpaceshipController1 : MonoBehaviour
{
    // Variable pública para controlar si el propulsor está en marcha
    public bool throttle = false;

    // Variables públicas para controlar la fuerza de cada movimiento y del propulsor
    public float pitchPower, rollPower, yawPower, enginePower;

    // Variable privada para almacenar la aceleración actual
    private float currentAcceleration = 0f;

    // Variables privadas para almacenar los movimientos relativos leídos de los input
    private float activeRoll, activePitch, activeYaw;

    // Referencia al InputAction de los controles (movimientos de ascenso, descenso y rotación)
    [SerializeField] InputActionReference moveAction;

    // Referencia al InputAction de los controles (aceleración)
    [SerializeField] InputActionReference accelAction;

    // Referencia al InputAction de los controles (deceleración)
    [SerializeField] InputActionReference deccelAction;

    // Referencia al InputAction de los controles (giro izquierdo)
    [SerializeField] InputActionReference leftYaw;

    // Referencia al InputAction de los controles  (giro derecho)
    [SerializeField] InputActionReference rightYaw;
    // Método Update que se ejecuta en cada frame del juego

    [SerializeField] GameObject leftWingFlap;

    private void Update()
    {
        // Obtenemos la dirección hacia la que se mueve el joystick
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();

        // Obtenemos el valor del eje de aceleración
        float accelerationInput = accelAction.action.ReadValue<float>();
        Debug.Log(moveDirection);
        // Obtenemos el valor del eje de deceleración
        float deccelerationInput = deccelAction.action.ReadValue<float>();

        // Obtenemos el valor del boton de giro izquierdo
        float leftYawInput = leftYaw.action.ReadValue<float>();

        // Obtenemos el valor del boton de giro derecho
        float rightYawInput = rightYaw.action.ReadValue<float>();

        // Si el gatillo izquierdo está presionado, reducimos la aceleración linealmente
        if (deccelerationInput > 0)
        {
            currentAcceleration -= deccelerationInput * Time.deltaTime; // Disminuir la aceleración linealmente
            currentAcceleration = Mathf.Clamp01(currentAcceleration); // Asegurar que esté en el rango de 0 a 1
        }
        if (accelerationInput > 0) {
            // Calculamos la aceleración actual basada en el valor del eje del gatillo derecho
            currentAcceleration += accelerationInput * Time.deltaTime;
            currentAcceleration = Mathf.Clamp01(currentAcceleration); // Asegurar que esté en el rango de 0 a 1
        }

        // Leemos el input que activa el propulsor
        if (currentAcceleration > 0 && throttle == false)
        {
            throttle = true;
        }
        else if (currentAcceleration == 0 && throttle == true)
        {
            throttle = false;
        }

        // Si el propulsor está activo
        if (throttle)
        {
            // Movemos la nave hacia adelante
            transform.position += transform.forward * enginePower * currentAcceleration * Time.deltaTime;

            // Elevamos/descendemos la nave si se usa el control arriba/abajo
            activePitch = moveDirection.y * pitchPower * Time.deltaTime;
            if (moveDirection.y < 0)
            {
                if (leftWingFlap.transform.localEulerAngles.x > 315 || leftWingFlap.transform.localEulerAngles.x < 180)
                    leftWingFlap.transform.Rotate(-5f, 0, 0);
            }
            if (moveDirection.y > 0)
            {
                if (leftWingFlap.transform.localEulerAngles.x < 45 || leftWingFlap.transform.localEulerAngles.x > 180)
                    leftWingFlap.transform.Rotate(5f, 0, 0);
            }
        }
        if (moveDirection.y == 0)
        {
            // Rotar de vuelta a la posición original
            if (leftWingFlap.transform.localEulerAngles.x != 172.183f)
            {
                float rotationAmount = 0f;
                if (leftWingFlap.transform.localEulerAngles.x > 180)
                    rotationAmount = -5f;
                else
                    rotationAmount = 5f;

                leftWingFlap.transform.Rotate(rotationAmount, 0, 0);
            }
            else
            {
                // Establecer la rotación exacta al valor original
                leftWingFlap.transform.localEulerAngles = new Vector3(172.183f, leftWingFlap.transform.localEulerAngles.y, leftWingFlap.transform.localEulerAngles.z);
            }
        }

        // Rotamos la nave si se usa el control izquierda/derecha
        activeRoll = moveDirection.x * rollPower * Time.deltaTime;

        // Aplicamos los cambios guardados en activePitch y activeRoll
        transform.Rotate(activePitch * pitchPower * Time.deltaTime,
            0f,
            -activeRoll * rollPower * Time.deltaTime,
            Space.Self);

        // Control de Yaw usando los botones de los hombros izquierdo y derecho

        if (leftYawInput > 0)
        {
            activeYaw = -yawPower * Time.deltaTime;
        }
        else if (rightYawInput > 0)
        {
            activeYaw = yawPower * Time.deltaTime;
        }
        else
        {
            activeYaw = 0f;
        }
        // Aplicamos el cambio de Yaw
        transform.Rotate(0f, activeYaw, 0f, Space.Self);
     
    }
}
