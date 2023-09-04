using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto que la c�mara seguir�.
    public float smoothSpeed = 5.0f; // Velocidad de seguimiento de la c�mara.
    public Vector3 offset = new Vector3(0, 0, -10); // Offset de la c�mara.

    private Vector3 initialPosition; // Posici�n inicial de la c�mara.
    private float currentXPosition; // Posici�n actual en X de la c�mara.
    private bool isCombat = false; // Indica si el jugador est� en combate.

    private bool inCombat = false; // Ajusta esto seg�n tu implementaci�n.

    void Start()
    {
        // Al comienzo, recordamos la posici�n inicial de la c�mara.
        initialPosition = transform.position;
        currentXPosition = initialPosition.x;
    }

    void Update()
    {
        // Verificar si el jugador est� en combate.
        isCombat = inCombat; // Ajusta esto seg�n tu implementaci�n.

        // Verificar el movimiento horizontal del jugador.
        float moveX = Input.GetAxis("Horizontal");

        // Si el jugador no est� en combate y se est� moviendo hacia la derecha, la c�mara lo sigue.
        if (!isCombat && currentXPosition <= target.position.x)
        {
            currentXPosition = target.position.x;
        }

        // Calcula la posici�n objetivo de la c�mara.
        Vector3 targetPosition = new Vector3(currentXPosition, initialPosition.y, initialPosition.z) + offset;

        // Suaviza el movimiento de la c�mara hacia la posici�n objetivo.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Establece la posici�n de la c�mara.
        transform.position = smoothedPosition;
    }
}
