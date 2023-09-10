using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BasicDefense : IDefense
{
    private bool isBlocking = false; // Variable para rastrear si se está bloqueando.
    private Transform armTransform;
    private Quaternion initialRotation;

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
        }

        // Rotar el brazo 90 grados en el eje Z para el bloqueo.
        armTransform.rotation = initialRotation * Quaternion.Euler(0f, 0f, 90f);
    }

    public void ReleaseBlock(Transform armTransform, bool facingRight)
    {
        if (isBlocking)
        {
            // Devolver el brazo a su posición de origen.
            armTransform.rotation = initialRotation;
            isBlocking = false; // Indicar que se ha finalizado el bloqueo.
        }
    }

    public int ReduceDamage()
    {
        return 0; // No se aplica reducción de daño durante el bloqueo.
    }

    public float GetBlockDuration()
    {
        return -1f; // La duración del bloqueo es indefinida mientras se mantiene presionado el botón.
    }
}
