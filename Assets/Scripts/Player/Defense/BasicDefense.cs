using System.Collections;
using UnityEngine;

public class BasicDefense : IDefense
{
    private bool isBlocking = false; // Variable para rastrear si se est� bloqueando.
    private Transform armTransform;
    private Quaternion initialRotation;
    private float blockDuration = 0.5f; // Duraci�n del bloqueo en segundos.

    public bool IsBlocking
    {
        get { return isBlocking; }
    }

    public void Block(Transform armTransform, bool facingRight)
    {
        this.armTransform = armTransform;

        if (!isBlocking)
        {
            isBlocking = true; // Indicar que se est� bloqueando.

            // Guardar la rotaci�n inicial del brazo.
            initialRotation = armTransform.rotation;

            // Iniciar la Coroutine para lerpear la rotaci�n.
            armTransform.GetComponent<MonoBehaviour>().StartCoroutine(BlockCoroutine());
        }
    }

    public void ReleaseBlock(Transform armTransform, bool facingRight)
    {
        if (isBlocking)
        {
            // Detener la Coroutine de bloqueo si est� en curso.
            armTransform.GetComponent<MonoBehaviour>().StopCoroutine(BlockCoroutine());

            // Iniciar la Coroutine para volver a la posici�n inicial.
            armTransform.GetComponent<MonoBehaviour>().StartCoroutine(UnblockCoroutine());

            isBlocking = false; // Indicar que se ha finalizado el bloqueo.
        }
    }

    private IEnumerator BlockCoroutine()
    {
        float timer = 0.0f;

        while (timer < blockDuration)
        {
            // Calcular la rotaci�n intermedia con interpolaci�n lerp.
            float progress = timer / blockDuration;
            Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);
            armTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Asegurarse de que la rotaci�n final sea exactamente la deseada.
        armTransform.rotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);
    }

    private IEnumerator UnblockCoroutine()
    {
        float timer = 0.0f;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);

        while (timer < blockDuration)
        {
            // Calcular la rotaci�n intermedia con interpolaci�n lerp (para volver a la posici�n inicial).
            float progress = timer / blockDuration;
            armTransform.rotation = Quaternion.Lerp(targetRotation, initialRotation, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Asegurarse de que la rotaci�n final sea exactamente la deseada (posici�n inicial).
        armTransform.rotation = initialRotation;
    }

    public int ReduceDamage()
    {
        return 0; // No se aplica reducci�n de da�o durante el bloqueo.
    }
}
