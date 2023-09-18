using TMPro;
using UnityEngine;

public class BasicBullet : MonoBehaviour, IBullet
{
    #region IBULLET_PROPERTIES
    public int Speed => _speed;
    public int Damage => _damage;
    public float LifeTime => _lifeTime;
    public bool EnemyDamaged => _enemyDamaged;
    public bool TravelRight => _travelRight;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private int _speed = 10;
    [SerializeField] private int _damage = 100;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private bool _enemyDamaged = false;
    [SerializeField] private bool _travelRight;
    #endregion

    private PlayerMovement playerMovement;
    private AttackController attackController;
    private GameObject touchedEnemy;

    private void Start()
    {
        // Inicializa la destrucción de la bala después del tiempo de vida.
        playerMovement = FindObjectOfType<PlayerMovement>();
        attackController = FindObjectOfType<AttackController>();
        _travelRight = playerMovement.isFacingRight;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        BulletTravel();
    }

    public void BulletTravel()
    {
        if (_travelRight)
        {
            transform.position += transform.right * Time.deltaTime * _speed;
        }
        else if(!_travelRight)
        {
            transform.position -= transform.right * Time.deltaTime * _speed;
        }
    }

    public void HandleEnemyCollision()
    {
        if (!_enemyDamaged)
        {
            // Obtén el GameObject del último enemigo tocado.
            GameObject enemyObject = touchedEnemy;
            if (enemyObject != null)
            {
                // Comprueba si el objeto tiene un componente CharacterStats.
                CharacterStats enemyStats = enemyObject.GetComponent<CharacterStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(_damage);
                    _enemyDamaged = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Si colisiona con un objeto que tiene el tag "Enemy", destruye la bala.
            touchedEnemy = collision.gameObject;
            HandleEnemyCollision();
            Destroy(gameObject);
        }
    }
}
