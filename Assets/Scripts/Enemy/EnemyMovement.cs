using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMoveable
{
    #region PUBLIC_PROPERTIES

    public float MovementSpeed => _movementSpeed;

    public float _movementSpeed = 2.5f; // Velocidad de movimiento del enemigo.

    public LayerMask groundLayer; // Capa del suelo.
    public Collider2D enemyCollider; // Collider del enemigo.
    public GameObject groundObject; // Collider del suelo

    public Transform playerTransform; // Transform del jugador para seguirlo.

    #endregion

    #region PRIVATE_PROPERTIES

    private Collider2D groundCollider;
    private bool isFacingRight = true; // Indica si el enemigo está mirando hacia la derecha.
    private bool playerIsClose = false;

    #endregion

    #region UNITY_EVENTS

    void Start()
    {
        // Obtener el collider del suelo (ground) desde el objeto asignado en el Inspector.
        groundCollider = groundObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador.
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Si la distancia es igual o menor a 1 unidad, imprimir un mensaje de depuración.
        if (distanceToPlayer <= 3.5f)
        {
            playerIsClose = true;
            Debug.Log("Jugador detectado");
        }
        else
        {
            playerIsClose= false;
        }

        // Movimiento horizontal y vertical hacia el jugador.
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        Vector2 movement = directionToPlayer * _movementSpeed * Time.deltaTime;

        // Restringir el movimiento del enemigo dentro del suelo.
        if (!playerIsClose)
        {
            Move(movement, groundCollider.bounds.min.x, groundCollider.bounds.max.x);
        }


        // Girar hacia la dirección del jugador.
        if (directionToPlayer.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (directionToPlayer.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    #endregion

    #region IMOVEABLE_METHODS

    public void Move(Vector2 direction, float minX, float maxX)
    {
        // Calcular la nueva posición del enemigo.
        Vector3 newPosition = transform.position + (Vector3)direction;

        // Restringir el movimiento del enemigo en X dentro del suelo.
        newPosition.x = Mathf.Clamp(newPosition.x, minX + enemyCollider.bounds.extents.x, maxX - enemyCollider.bounds.extents.x);

        // Aplicar la nueva posición.
        transform.position = newPosition;
    }

    #endregion

    #region PRIVATE_METHODS

    private void Flip()
    {
        // Cambiar la dirección del enemigo al reflejarlo horizontalmente.
        isFacingRight = !isFacingRight;

        // Obtener la escala actual.
        Vector3 scale = transform.localScale;

        // Reflejar horizontalmente al enemigo.
        scale.x *= -1;

        // Aplicar la nueva escala.
        transform.localScale = scale;
    }

    #endregion
}
