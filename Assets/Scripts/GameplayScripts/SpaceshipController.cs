using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// SCRIPT para realizar los movimientos de la nave espacial
// mediante el nuevo Input Syste de Unity
public class SpaceshipController : MonoBehaviour {

    // Variable p�blica para controlar si el propulsor est� en marcha
    public bool throttle = false;

    // Variables p�blicas para controlar la fuerza de cada movimiento y del propulsor
    public float pitchPower, rollPower, yawPower, enginePower;

    // Variables privadas para almacenar los movimientos relativos le�dos de los input
    private float activeRoll, activePitch, activeYaw;

    // Referencia al InputAction de los controles (movimientos de ascenso, descenso y rotaci�n)
    [SerializeField] InputActionReference moveAction;

    // Referencia al InputAction de los controles (movimiento lateral)
    [SerializeField] InputActionReference moveActionYaw;

    // Referencia al InputAction de los controles (aceleraci�n)
    [SerializeField] InputActionReference accelAction;

    // Variable para detectar solo la primera pulsaci�n
    private bool firstTime = true;

    // M�todo Update que se ejecuta en cada frame del juego
    private void Update() {

        // Obtenemos la direcci�n hacia la que se mueve el joystick
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();
        Vector2 moveDirectionYaw = moveActionYaw.action.ReadValue<Vector2>();

        // Obtenemos el valor del bot�n
        bool isButtonPressed = (accelAction.action.ReadValue<float>() == 1);

        // Leemos el input que activa el propulsor
        if (isButtonPressed && firstTime) {
            firstTime = false; // Desactivamos la primera pulsaci�n
            if (!throttle) throttle = true; // Lo activamos
            else throttle = false; // Lo desactivamos
            
        }

        // Si el propulsor est� activo
        if (throttle) {

            // Movemos la nave hacia adelante
            transform.position += transform.forward * enginePower * Time.deltaTime;

            // Elevamos/descendemos la nave si se usa el control arriba/abajo
            activePitch = moveDirection.y * pitchPower * Time.deltaTime;

            // Rotamos la nave si se usa el control izquierda/derecha
            activeRoll = moveDirection.x * rollPower * Time.deltaTime;

            // Movemos lateralmente la nave si se usa el segundo control (solo los controles laterales)
            activeYaw = moveDirectionYaw.x * yawPower * Time.deltaTime;

            // Aplicamos los cambios guardados en activePitch, activeRoll y activeYaw
            transform.Rotate(activePitch * pitchPower * Time.deltaTime, 
                activeYaw * yawPower * Time.deltaTime, 
                -activeRoll * rollPower * Time.deltaTime, 
                Space.Self);

        }
    }
}