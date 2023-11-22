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

        // Iniciar la rutina para guardar la posición y rotación del jugador cada segundo.
        StartCoroutine(SavePlayerSnapshotRoutine());
    }

    private void Update()
    {
        timeTravelCooldown -= Time.deltaTime;
        // Si se presiona la tecla T, activar la función de viaje en el tiempo.
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeTravel();
        }
    }

    private void TimeTravel()
    {
        // Verificar si hay suficientes instantáneas en la cola de eventos para retroceder 4 segundos.
        int snapshotsToSkip = Mathf.RoundToInt(timeTravelCooldown);

        if (timeTravelCooldown <= 0)
        {
            // Retroceder 4 segundos.
            for (int i = 0; i < snapshotsToSkip - 1; i++)
            {
                eventQueue.Dequeue();
            }

            // Recuperar la instantánea correspondiente a 4 segundos atrás.
            TimeSnapshot snapshot = eventQueue.Dequeue();

            // Aplicar la posición y rotación almacenadas en la instantánea.
            playerMovement.RestoreSnapshot(snapshot);

            // Mostrar un mensaje de depuración con la posición restaurada.
            Debug.Log("Posición restaurada: " + snapshot.Position);

            // Vaciar la lista de instantáneas después de usar el viaje en el tiempo.
            eventQueue.Clear();

            timeTravelCooldown = 4f;
        }
        else
        {
            Debug.Log("No hay suficientes instantáneas disponibles para retroceder en el tiempo.");
        }
    }

    private IEnumerator SavePlayerSnapshotRoutine()
    {
        while (true)
        {
            // Guardar la posición y rotación actual del jugador en una instantánea.
            TimeSnapshot snapshot = new TimeSnapshot();
            snapshot.Position = playerMovement.transform.position;
            snapshot.Rotation = playerMovement.transform.rotation;

            // Agregar la instantánea a la cola de eventos.
            eventQueue.Enqueue(snapshot);

            // Mostrar un mensaje de depuración con la posición guardada.
            Debug.Log("Posición guardada: " + snapshot.Position);

            // Esperar 1 segundo antes de tomar otra instantánea.
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
