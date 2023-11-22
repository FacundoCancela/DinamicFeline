using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelManager : MonoBehaviour
{
    private Queue<TimeSnapshot> eventQueue = new Queue<TimeSnapshot>();
    private PlayerMovement playerMovement;

    [SerializeField] private float timeTravelCooldown = 4f;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Iniciar la rutina para guardar la posici�n y rotaci�n del jugador cada segundo.
        StartCoroutine(SavePlayerSnapshotRoutine());
    }

    private void Update()
    {
        timeTravelCooldown -= Time.deltaTime;
        // Si se presiona la tecla T, activar la funci�n de viaje en el tiempo.
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeTravel();
        }
    }

    private void TimeTravel()
    {
        // Verificar si hay suficientes instant�neas en la cola de eventos para retroceder 4 segundos.
        int snapshotsToSkip = Mathf.RoundToInt(timeTravelCooldown);

        if (timeTravelCooldown <= 0)
        {
            // Retroceder 4 segundos.
            for (int i = 0; i < snapshotsToSkip - 1; i++)
            {
                eventQueue.Dequeue();
            }

            // Recuperar la instant�nea correspondiente a 4 segundos atr�s.
            TimeSnapshot snapshot = eventQueue.Dequeue();

            // Aplicar la posici�n y rotaci�n almacenadas en la instant�nea.
            playerMovement.RestoreSnapshot(snapshot);

            // Mostrar un mensaje de depuraci�n con la posici�n restaurada.
            Debug.Log("Posici�n restaurada: " + snapshot.Position);

            // Vaciar la lista de instant�neas despu�s de usar el viaje en el tiempo.
            eventQueue.Clear();

            timeTravelCooldown = 4f;
        }
        else
        {
            Debug.Log("No hay suficientes instant�neas disponibles para retroceder en el tiempo.");
        }
    }

    private IEnumerator SavePlayerSnapshotRoutine()
    {
        while (true)
        {
            // Guardar la posici�n y rotaci�n actual del jugador en una instant�nea.
            TimeSnapshot snapshot = new TimeSnapshot();
            snapshot.Position = playerMovement.transform.position;
            snapshot.Rotation = playerMovement.transform.rotation;

            // Agregar la instant�nea a la cola de eventos.
            eventQueue.Enqueue(snapshot);

            // Mostrar un mensaje de depuraci�n con la posici�n guardada.
            Debug.Log("Posici�n guardada: " + snapshot.Position);

            // Esperar 1 segundo antes de tomar otra instant�nea.
            yield return new WaitForSeconds(1f);
        }
    }

}

[System.Serializable]
public class TimeSnapshot
{
    public Vector3 Position;
    public Quaternion Rotation;
}
