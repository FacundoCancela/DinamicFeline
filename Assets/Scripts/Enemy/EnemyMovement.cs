using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMoveable
{
    #region PUBLIC_PROPERTIES

    public float MovementSpeed => _movementSpeed;

    public float _movementSpeed = 2.5f; // Velocidad de movimiento del enemigo.

    public LayerMask groundLayer; // Capa del suelo.
    public Collider2D enemyCollider; // Collider del enemigo.
    public Transform playerTransform; // Transform del jugador para seguirlo.
    public BasicEnemyAttackController EnemyAttackController;
    public EnemyTypeObject enemyType;


    #endregion

    #region PRIVATE_PROPERTIES

    private Collider2D groundCollider;
    public bool isFacingRight = true; // Indica si el enemigo está mirando hacia la derecha.
    public bool playerIsClose = false;

    GrafoManager grafo;
    [SerializeField] int currentNode = 0;
    int nextPos = 0;
    Vector3 newPosition;
    Vector3 direction;
    Animator anim;
    [SerializeField] int UpdatePos;
    nodos[] camino;
    int availableNodes;

    [SerializeField] LayerMask layers;

    #endregion

    #region UNITY_EVENTS

    void Start()
    {
        GameObject gameObject = GameObject.Find("GrafoManager");
        grafo = gameObject.GetComponent<GrafoManager>();
        anim = GetComponent<Animator>();
        // Buscar el objeto con el script PlayerMovement y asignar su transform al playerTransform.
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerTransform = playerMovement.transform;
        }

        // Buscar el objeto con el tag "Ground" y asignar su collider al groundCollider.
        GameObject groundObject = GameObject.FindGameObjectWithTag("Ground");
        if (groundObject != null)
        {
            groundCollider = groundObject.GetComponent<Collider2D>();
        }

        camino = new nodos[grafo.nodos.Length];

        currentNode = 0;

        camino = grafo.PathFinding(currentNode);
        currentNode = camino[0].IdNode;
        UpdatePos = currentNode;

        foreach (var item in camino)
        {
            availableNodes++;
        }

        CalculatePath();
    }

    void Update()
    {

           


            // Calcular la distancia entre el enemigo y el jugador.
       float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Si la distancia es igual o menor a 1 unidad, imprimir un mensaje de depuración.
        if (distanceToPlayer <= 2.8f)
        {
            anim.SetBool("isWalking", false);
            playerIsClose = true;
        }
        else
        {
            playerIsClose = false;
        }

        newPosition = grafo.GetPosition(newPosition, camino[currentNode].IdNode);
        direction = newPosition - transform.position;
        // Movimiento horizontal y vertical hacia el jugador.
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        Vector2 movement = direction.normalized * _movementSpeed * Time.deltaTime;

        

        // Restringir el movimiento del enemigo dentro del suelo.
        if (!playerIsClose && !EnemyAttackController.IsAttacking)
        {
       
            Move(movement, groundCollider.bounds.min.y, groundCollider.bounds.max.y);
        }
        else if (currentNode != 5)
        {
            Move(movement, groundCollider.bounds.min.y, groundCollider.bounds.max.y);
        }
        if(_movementSpeed != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
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


        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }


        if (playerIsClose && currentNode == 5)
        {
            anim.SetBool("isWalking", false);
        }
    }

    #endregion

    #region IMOVEABLE_METHODS

    public void Move(Vector2 direction, float minY, float maxY)
    {
        // Calcular la nueva posición del enemigo.
        Vector3 newPosition = transform.position + (Vector3)direction;

        // Restringir el movimiento del enemigo en X dentro del suelo.
        newPosition.y = Mathf.Clamp(newPosition.y, minY - enemyCollider.bounds.extents.y, maxY + enemyCollider.bounds.extents.y);
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

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.GetComponent<nodos>() != null)
            {
                if (collision.gameObject.GetComponent<nodos>().IdNode == currentNode)
                {
                    anim.SetBool("isWalking", false);
                    _movementSpeed = 0;
                    if (camino[currentNode].IdNode <= 4)
                    {
                        if (!camino[currentNode + 1].occupied)
                        {
                            CalculatePath();
                            _movementSpeed = 2.5f;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _movementSpeed = 2.5f;
            if (collision.gameObject.GetComponent<nodos>() != null)
            {
                if (collision.gameObject.GetComponent<nodos>().IdNode == currentNode)
                {
                    if (camino[currentNode].IdNode <= 4)
                    {
                        if (!camino[currentNode + 1].occupied)
                        {
                            CalculatePath();
                            _movementSpeed = 2.5f;
                        }
                    }

                    else
                    {
                        _movementSpeed = 2.5f;
                    }
                }
            }
        }
    }
    #endregion

    private nodos[] CalculateDijkstra(nodos [] nodos)
    {
        nodos = grafo.PathFinding(currentNode);

        return nodos;
    }


    private void CalculatePath()
    {
        if (UpdatePos != availableNodes - 1)
        {
           
            UpdatePos++;
            if (!grafo.nodos[UpdatePos].occupied)
            {
                grafo.nodos[currentNode].occupied = false;
                nextPos = camino[UpdatePos].IdNode;

                newPosition = grafo.GetPosition(newPosition, camino[nextPos].IdNode);


                currentNode = camino[nextPos].IdNode;


                direction = newPosition - transform.position;
                grafo.nodos[currentNode].occupied = true;
                Debug.Log(grafo.nodos[currentNode].occupied);
            }

            if(UpdatePos >= 5)
            {
                UpdatePos = 0;
            }
        }
    }


    public void Attack()
    {
        GetComponentInChildren<BasicEnemyAttackController>().PerformAttack();
    }
    public void Death()
    {
        grafo.nodos[currentNode].occupied = false;
        playerIsClose = false;
    }

    #endregion
}
