using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type Object")]
public class EnemyTypeObject : ScriptableObject
{
    [Header("General Settings")]
    public string TypeName; // Nombre del tipo de enemigo.
    public GameObject Prefab; // Prefab del enemigo.

    [Header("Movement Settings")]
    public float MovementSpeed = 2.5f; // Velocidad de movimiento del enemigo.
    public float MinX; // Límite mínimo en el eje X.
    public float MaxX; // Límite máximo en el eje X.
    public LayerMask GroundLayer; // Capa del suelo.
    public Collider2D EnemyCollider; // Collider del enemigo.

    [Header("Attack Settings")]
    public IAttackStrategy AttackStrategy; // Estrategia de ataque del enemigo.
    public float AttackInterval = 3f; // Intervalo entre ataques en segundos.

    [Header("Player Detection")]
    public bool IsFacingRight = true; // Indica si el enemigo está mirando hacia la derecha.
    public Transform PlayerTransform; // Transform del jugador para seguirlo.
    public bool PlayerIsClose = false; // Indica si el jugador está cerca.

    [Header("References")]
    public IsTouchingPlayer IsTouchingPlayer; // Referencia al componente de detección de jugador.
    public CharacterStats CharacterStats; // Referencia a las estadísticas del enemigo.
}


