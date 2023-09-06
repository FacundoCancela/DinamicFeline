using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private IDefense currentDefenseStrategy;
    private bool isBlocking = false; // Variable para rastrear si se est� bloqueando.
    public PlayerMovement playerMovement; // Referencia al PlayerMovement.

    public bool IsBlocking
    {
        get { return isBlocking; }
    }

    // Asigna la referencia al PlayerMovement en el Inspector.
    public PlayerMovement PlayerMovement
    {
        get { return playerMovement; }
        set { playerMovement = value; }
    }

    public void SetDefenseStrategy(IDefense strategy)
    {
        currentDefenseStrategy = strategy;
    }

    public void PerformBlock()
    {
        if (!isBlocking && currentDefenseStrategy != null)
        {
            StartCoroutine(BlockCoroutine());
        }
    }

    public void ReleaseBlock()
    {
        if (isBlocking)
        {
            // Puedes agregar l�gica adicional aqu� si es necesario.
            isBlocking = false;
            // Finaliza el bloqueo (por ejemplo, restaura la posici�n inicial del brazo).
            currentDefenseStrategy.ReleaseBlock(transform, IsFacingRight());
        }
    }

    private IEnumerator BlockCoroutine()
    {
        isBlocking = true; // Indicar que se est� bloqueando.

        currentDefenseStrategy.Block(transform, IsFacingRight());

        // No olvides agregar una l�gica para finalizar el bloqueo cuando se suelte la tecla C.
        // Por ahora, hemos agregado ReleaseBlock como una forma de finalizar el bloqueo.

        yield return null;
    }

    private bool IsFacingRight()
    {
        // Verificar la direcci�n en la que el jugador est� mirando usando la referencia a PlayerMovement.
        return playerMovement != null && playerMovement.isFacingRight;
    }
}
