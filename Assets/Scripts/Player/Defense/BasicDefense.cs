using System.Collections;
using UnityEngine;

public class BasicDefense : IDefense
{
    private bool isBlocking = false; // Variable para rastrear si se está bloqueando.
    private Transform armTransform;
    private Quaternion initialRotation;
    private float blockDuration = 0.5f; // Duración del bloqueo en segundos.

    public bool IsBlocking
    {
        get { return isBlocking; }
    }

    public void Block(Transform armTransform, bool facingRight)
    {
        this.armTransform = armTransform;

        if (!isBlocking)
        {
            isBlocking = true; // Indicar que se está bloqueando.

            // Guardar la rotación inicial del brazo.
            initialRotation = armTransform.rotation;

            // Iniciar la Coroutine para lerpear la rotación.
            armTransform.GetComponent<MonoBehaviour>().StartCoroutine(BlockCoroutine());
        }
    }

    public void ReleaseBlock(Transform armTransform, bool facingRight)
    {
        if (isBlocking)
        {
            // Detener la Coroutine de bloqueo si está en curso.
            armTransform.GetComponent<MonoBehaviour>().StopCoroutine(BlockCoroutine());

            // Iniciar la Coroutine para volver a la posición inicial.
            armTransform.GetComponent<MonoBehaviour>().StartCoroutine(UnblockCoroutine());

            isBlocking = false; // Indicar que se ha finalizado el bloqueo.
        }
    }

    private IEnumerator BlockCoroutine()
    {
        float timer = 0.0f;

        while (timer < blockDuration)
        {
            // Calcular la rotación intermedia con interpolación lerp.
            float progress = timer / blockDuration;
            Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);
            armTransform.rotation = Quaternion.Lerp(initialRotation, targetRotation, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Asegurarse de que la rotación final sea exactamente la deseada.
        armTransform.rotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);
    }

    private IEnumerator UnblockCoroutine()
    {
        float timer = 0.0f;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);

        while (timer < blockDuration)
        {
            // Calcular la rotación intermedia con interpolación lerp (para volver a la posición inicial).
            float progress = timer / blockDuration;
            armTransform.rotation = Quaternion.Lerp(targetRotation, initialRotation, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Asegurarse de que la rotación final sea exactamente la deseada (posición inicial).
        armTransform.rotation = initialRotation;
    }

    public int ReduceDamage()
    {
        return 0; // No se aplica reducción de daño durante el bloqueo.
    }
}
