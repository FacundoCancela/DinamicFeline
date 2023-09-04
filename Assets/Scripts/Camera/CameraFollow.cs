using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto que la cámara seguirá.
    public float smoothSpeed = 5.0f; // Velocidad de seguimiento de la cámara.
    public Vector3 offset = new Vector3(0, 0, -10); // Offset de la cámara.

    private Vector3 initialPosition; // Posición inicial de la cámara.
    private float currentXPosition; // Posición actual en X de la cámara.
    private bool isCombat = false; // Indica si el jugador está en combate.

    private bool inCombat = false; // Ajusta esto según tu implementación.

    void Start()
    {
        // Al comienzo, recordamos la posición inicial de la cámara.
        initialPosition = transform.position;
        currentXPosition = initialPosition.x;
    }

    void Update()
    {
        // Verificar si el jugador está en combate.
        isCombat = inCombat; // Ajusta esto según tu implementación.

        // Verificar el movimiento horizontal del jugador.
        float moveX = Input.GetAxis("Horizontal");

        // Si el jugador no está en combate y se está moviendo hacia la derecha, la cámara lo sigue.
        if (!isCombat && currentXPosition <= target.position.x)
        {
            currentXPosition = target.position.x;
        }

        // Calcula la posición objetivo de la cámara.
        Vector3 targetPosition = new Vector3(currentXPosition, initialPosition.y, initialPosition.z) + offset;

        // Suaviza el movimiento de la cámara hacia la posición objetivo.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Establece la posición de la cámara.
        transform.position = smoothedPosition;
    }
}
