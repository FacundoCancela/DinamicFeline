using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region PUBLIC_PROPERTIES

    public float MovementSpeed => _movementSpeed;

    public float _movementSpeed = 5.0f; // Velocidad de movimiento del jugador.

    public LayerMask groundLayer; // Capa del suelo.
    public Collider2D playerCollider; // Collider del jugador.
    public GameObject groundObject; // Objeto del suelo.
    public Camera mainCamera; // Referencia a la cámara principal.
    public AttackController attackController;
    public Stats stats;

    #endregion

    #region PRIVATE_PROPERTIES
    Animator animator;
    private Collider2D groundCollider;
    private float cameraHalfWidth;
    public bool isFacingRight = true; // Indica si el personaje está mirando hacia la derecha.

    private float Gravedad;
    private float YPos;
    private float YposPiso;
    public bool InGround;
    public bool Saltando;
    public int Fases;
    public float AlturaSalto;
    public float PotenciaSalto;
    public float Fallen;
    private int sky;

    #endregion

    #region UNITY_EVENTS

    void Start()
    {
        // Obtener el collider del suelo (ground) desde el objeto asignado en el Inspector.
        groundCollider = groundObject.GetComponent<Collider2D>();

        // Calcular el ancho medio de la cámara en unidades del mundo.
        cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);


        animator = GetComponent<Animator>();

        _movementSpeed = stats.speed;
    }

    void Update()
    {
        // Verificar si el jugador no está atacando antes de permitir el movimiento.
        if (!attackController.IsAttacking)
        {
            // Movimiento horizontal y vertical.
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if ((moveX != 0 || moveY != 0))
            {
                animator.SetBool("isWalking", true);
            }
            else animator.SetBool("isWalking", false);




            Vector2 movement = new Vector2(moveX, moveY).normalized * _movementSpeed * Time.deltaTime;

            // Calcular las coordenadas de la cámara.
            float cameraX = mainCamera.transform.position.x;

            // Calcular los límites del movimiento en X dentro de la vista de la cámara.
            float minX = cameraX - cameraHalfWidth;
            float maxX = cameraX + cameraHalfWidth;

            // Aplicar movimiento al jugador con restricciones en X.
            Move(movement, minX, maxX);
        }

        DetectorDePiso();
        Jump();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Gravedad * Time.deltaTime);
    }

    #endregion

    #region IMOVEABLE_METHODS

    public void Move(Vector2 direction, float minX, float maxX)
    {
        // Calcular las restricciones en X.
        float playerHalfWidth = playerCollider.bounds.extents.x;
        float minY = groundCollider.bounds.min.y + playerCollider.bounds.extents.y;
        float maxY = groundCollider.bounds.max.y + playerCollider.bounds.extents.y;

        // Calcular la nueva posición del jugador.
        Vector3 newPosition = transform.position + (Vector3)direction;

        // Restringir el movimiento en X dentro de los límites de la cámara.
        newPosition.x = Mathf.Clamp(newPosition.x, minX + playerHalfWidth, maxX - playerHalfWidth);

        // Restringir el movimiento en Y, teniendo en cuenta las restricciones del suelo.
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Cambiar la escala del objeto del jugador para reflejarlo horizontalmente si es necesario.
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        transform.position = newPosition;


    } 

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Saltando && InGround)
        {
            YposPiso = transform.position.y;
            Saltando = true; 
            InGround = false;
        }

        if (Saltando)
        {
            switch(Fases)
            {
                case 0:
                    Gravedad = AlturaSalto;
                    Fases = 1;

                    break;

                case 1:
                    if (Gravedad > 0)
                    {
                        Gravedad -= PotenciaSalto * Time.deltaTime;
                    }
                    else Fases = 2;

                    break;
            }
        }

    }

    void SetTransformY(float n)
    {
        transform.position = new Vector3(transform.position.x, n, transform.position.z);
    }

    public void DetectorDePiso()
    {
        if(!Saltando)
        {
            sky = 0;

            if(YPos == YposPiso)
            {
                InGround = true;
            }
        }

        if(InGround)
        {
            Gravedad = 0;
            Fases = 0;
        }
        else
        {
            switch(Fases)
            {
                case 2:
                    Gravedad = 0;
                    Fases = 3;
                    break;

                case 3:
                    if(YPos >= YposPiso)
                    {
                        if(Gravedad > -10)
                        {
                            Gravedad -= AlturaSalto / Fallen * Time.deltaTime;
                        }
                    }
                    else 
                    {
                        Saltando = false;
                        InGround = true;
                        SetTransformY(YposPiso);
                        Fases = 0;
                    }
                    break;
            }
        }

        YPos = transform.position.y;
    }


    private void Flip()
    {
        // Cambiar la dirección del personaje al reflejarlo horizontalmente.
        isFacingRight = !isFacingRight;

        // Obtener la escala actual.
        Vector3 scale = transform.localScale;

        // Reflejar horizontalmente el objeto del jugador.
        scale.x *= -1;

        // Aplicar la nueva escala.
        transform.localScale = scale;
    }

    #endregion

    public void RestoreSnapshot(TimeSnapshot snapshot)
    {
        // Restaurar la posición y rotación del jugador desde la instantánea.
        transform.position = snapshot.Position;
        transform.rotation = snapshot.Rotation;
    }


}
