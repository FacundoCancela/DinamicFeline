using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackStrategy : IAttackStrategy
{
    private int damage = 20; // Daño del golpe básico.
    private float attackDuration = 0.5f; // Duración total del ataque en segundos (ida y vuelta).
    private bool isAttacking = false; // Variable para rastrear si se está realizando un ataque.

    public bool IsAttacking
    {
        get { return isAttacking; }
    }

    public void Attack(Transform armTransform)
    {
        if (!isAttacking)
        {
            isAttacking = true; // Indicar que se está atacando.

            // Guardar la posición inicial del brazo.
            Vector3 initialArmPosition = armTransform.position;

            // Iniciar la animación del brazo como una coroutina.
            armTransform.GetComponent<MonoBehaviour>().StartCoroutine(PerformAttackAnimation(armTransform, initialArmPosition));
        }
    }

    private IEnumerator PerformAttackAnimation(Transform armTransform, Vector3 initialArmPosition)
    {
        float timer = 0.0f;

        // Realizar el movimiento del brazo durante el ataque (ida).
        while (timer < attackDuration / 2)
        {
            // Calcular la nueva posición del brazo con interpolación lineal.
            float progress = timer / (attackDuration / 2);
            Vector3 targetPosition = initialArmPosition + new Vector3(-2f,-0.25f, 0);
            armTransform.position = Vector3.Lerp(initialArmPosition, targetPosition, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Realizar el movimiento del brazo durante el ataque (vuelta).
        timer = 0.0f; // Reiniciar el temporizador.

        while (timer < attackDuration / 2)
        {
            // Calcular la nueva posición del brazo con interpolación lineal.
            float progress = timer / (attackDuration / 2);
            Vector3 targetPosition = initialArmPosition;
            armTransform.position = Vector3.Lerp(initialArmPosition + new Vector3(-2f,-0.25f, 0), targetPosition, progress);

            // Actualizar el temporizador.
            timer += Time.deltaTime;

            // Esperar hasta el siguiente frame.
            yield return null;
        }

        // Restablecer la posición del brazo a su posición inicial.
        armTransform.position = initialArmPosition;
        isAttacking = false; // Indicar que se ha completado el ataque.
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }
}