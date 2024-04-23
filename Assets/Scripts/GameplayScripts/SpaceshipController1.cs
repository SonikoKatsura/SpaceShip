using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// SCRIPT para realizar los movimientos de la nave espacial
// mediante el nuevo Input System de Unity
public class SpaceshipController1 : MonoBehaviour
{
    // Variable p�blica para controlar si el propulsor est� en marcha
    public bool throttle = false;

    // Variables p�blicas para controlar la fuerza de cada movimiento y del propulsor
    public float pitchPower, rollPower, yawPower, enginePower;

    // Variable privada para almacenar la aceleraci�n actual
    [SerializeField] float currentAcceleration = 0f;

    // Variables privadas para almacenar los movimientos relativos le�dos de los input
    [SerializeField] float activeRoll, activePitch, activeYaw;

    // Referencia al InputAction de los controles (movimientos de ascenso, descenso y rotaci�n)
    [SerializeField] InputActionReference moveAction;

    // Referencia al InputAction de los controles (aceleraci�n)
    [SerializeField] InputActionReference accelAction;

    // Referencia al InputAction de los controles (deceleraci�n)
    [SerializeField] InputActionReference deccelAction;

    // Referencia al InputAction de los controles (giro izquierdo)
    [SerializeField] InputActionReference leftYaw;

    // Referencia al InputAction de los controles  (giro derecho)
    [SerializeField] InputActionReference rightYaw;
    // M�todo Update que se ejecuta en cada frame del juego

    [SerializeField] GameObject leftWingFlap;
    [SerializeField] GameObject rightWingFlap;
    [SerializeField] GameObject backFlapR;
    [SerializeField] GameObject backFlapL;



    private void OnTriggerEnter(Collider other) // Nuevo
    {
        if (other.CompareTag("Ring")) // Aseg�rate de que el collider sea el del anillo
        {
            currentAcceleration = 2f;
        }
    }
    private void Update()
    {
        // Obtenemos la direcci�n hacia la que se mueve el joystick
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();

        // Obtenemos el valor del eje de aceleraci�n
        float accelerationInput = accelAction.action.ReadValue<float>();

        // Obtenemos el valor del eje de deceleraci�n
        float deccelerationInput = deccelAction.action.ReadValue<float>();

        // Obtenemos el valor del boton de giro izquierdo
        float leftYawInput = leftYaw.action.ReadValue<float>();

        // Obtenemos el valor del boton de giro derecho
        float rightYawInput = rightYaw.action.ReadValue<float>();

        // Si el gatillo izquierdo est� presionado, reducimos la aceleraci�n linealmente
        if (deccelerationInput > 0)
        {
            currentAcceleration -= deccelerationInput * Time.deltaTime; // Disminuir la aceleraci�n linealmente
            currentAcceleration = Mathf.Clamp(currentAcceleration, 0.1f, 1f); // Asegurar que est� en el rango de 0 a 1
        }
        if (accelerationInput > 0) {
            // Calculamos la aceleraci�n actual basada en el valor del eje del gatillo derecho
            currentAcceleration += accelerationInput * Time.deltaTime;
            currentAcceleration = Mathf.Clamp(currentAcceleration, 0.1f, 1f); // Asegurar que est� en el rango de 0 a 1
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

        // Variables para controlar la posici�n del flap del ala
        bool flapUp = false;
        bool flapDown = false;
        bool flapRight = false;
        bool flapLeft = false;

        // Si el propulsor est� activo
        if (throttle)
        {
            // Movemos la nave hacia adelante
            transform.position += transform.forward * enginePower * currentAcceleration * Time.deltaTime;

            // Elevamos/descendemos la nave si se usa el control arriba/abajo
            activePitch = moveDirection.y * pitchPower * Time.deltaTime;

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
                backFlapL.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapL.transform.localEulerAngles.z + 5f, 0f, 75f));
                backFlapR.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapR.transform.localEulerAngles.z + 5f, 0f, 75f));
            }
        else if (rightYawInput > 0)
        {
            activeYaw = yawPower * Time.deltaTime;
                backFlapL.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapL.transform.localEulerAngles.z + 5f, 0f, -75f));
                backFlapR.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapR.transform.localEulerAngles.z + 5f, 0f, -75f));
            }
        else
        {
            activeYaw = 0f;
                backFlapL.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapL.transform.localEulerAngles.z - 5f, 0f, 0f));
                backFlapR.transform.localRotation = Quaternion.Euler(172, 0f, Mathf.Clamp(backFlapR.transform.localEulerAngles.z - 5f, 0f, 0f));
            }
        // Aplicamos el cambio de Yaw
        transform.Rotate(0f, activeYaw, 0f, Space.Self);

            // Control de movimiento del flap
            if (moveDirection.y < 0)
            {
                flapDown = true;
                flapUp = false;
                flapRight = false;
                flapLeft = false;
            }
            else if (moveDirection.y > 0)
            {
                flapUp = true;
                flapDown = false;
                flapRight = false;
                flapLeft = false;
            }
            else if (moveDirection.x < 0)
            {
                flapUp = false;
                flapDown = false;
                flapRight = false;
                flapLeft = true;
            }

            else if (moveDirection.x > 0)
            {
                flapUp = false;
                flapDown = false;
                flapRight = true;
                flapLeft = false;
            }
            else
            {
                flapUp = false;
                flapDown = false;
                flapRight = false;
                flapLeft = false;
            }

            // Rotaci�n del flap izquierdo basado en su estado
            if (flapDown && (leftWingFlap.transform.localEulerAngles.x > 315 || leftWingFlap.transform.localEulerAngles.x < 180))
            {
                leftWingFlap.transform.Rotate(-5f, 0, 0);
            }
            else if (flapUp && (leftWingFlap.transform.localEulerAngles.x < 45 || leftWingFlap.transform.localEulerAngles.x > 180))
            {
                leftWingFlap.transform.Rotate(5f, 0, 0);
            }
            else if (flapLeft && (leftWingFlap.transform.localEulerAngles.x < 45 || leftWingFlap.transform.localEulerAngles.x > 180))
            {
                leftWingFlap.transform.Rotate(5f, 0, 0);
            }
            else if (flapRight && (leftWingFlap.transform.localEulerAngles.x > 315 || leftWingFlap.transform.localEulerAngles.x < 180))
            {
                leftWingFlap.transform.Rotate(-5f, 0, 0);
            }
            else if (!flapUp && !flapDown && !flapLeft && !flapRight && leftWingFlap.transform.localEulerAngles.x != 0)
            {
                // Si no se est� moviendo en ninguna direcci�n, y el flap no est� en su posici�n original
                // Lo rotamos hacia su posici�n original
                if (leftWingFlap.transform.localEulerAngles.x > 180)
                {
                    leftWingFlap.transform.Rotate(-5f, 0, 0);
                }
                else if (leftWingFlap.transform.localEulerAngles.x < 180 && leftWingFlap.transform.localEulerAngles.x > 0)
                {
                    leftWingFlap.transform.Rotate(5f, 0, 0);
                }
            }
            // Rotaci�n del flap derecho basado en su estado
            if (flapDown && (rightWingFlap.transform.localEulerAngles.x > 315 || rightWingFlap.transform.localEulerAngles.x < 180))
            {
                rightWingFlap.transform.Rotate(-5f, 0, 0);
            }
            else if (flapUp && (rightWingFlap.transform.localEulerAngles.x < 45 || rightWingFlap.transform.localEulerAngles.x > 180))
            {
                rightWingFlap.transform.Rotate(5f, 0, 0);
            }            
            else if (flapLeft && (rightWingFlap.transform.localEulerAngles.x > 315 || rightWingFlap.transform.localEulerAngles.x < 180))
            {
                rightWingFlap.transform.Rotate(-5f, 0, 0);
            }

            else if (flapRight && (rightWingFlap.transform.localEulerAngles.x < 45 || rightWingFlap.transform.localEulerAngles.x > 180))
            {
                rightWingFlap.transform.Rotate(5f, 0, 0);
            }

            else if (!flapUp && !flapDown && !flapLeft && !flapRight && rightWingFlap.transform.localEulerAngles.x != 0)
            {
                // Si no se est� moviendo en ninguna direcci�n, y el flap no est� en su posici�n original
                // Lo rotamos hacia su posici�n original
                if (rightWingFlap.transform.localEulerAngles.x > 180)
                {
                    rightWingFlap.transform.Rotate(-5f, 0, 0);
                }
                else if (rightWingFlap.transform.localEulerAngles.x < 180 && rightWingFlap.transform.localEulerAngles.x > 0)
                {
                    rightWingFlap.transform.Rotate(5f, 0, 0);
                }
            }
        }
     
    }
}
