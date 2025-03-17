using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento del jugador")]
    public CharacterController controller;
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    
    [Header("Cámara del jugador")]
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    [Header("Detección de suelo")]
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    [Header("Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Agarre de bordes")]
    public Transform edgeCheck;
    public float edgeGrabDistance = 0.5f;
    public bool isHanging = false;

    //Checkpoint
    private Vector3 lastCheckpointPosition;

        public void GuardarCheckpoint(Vector3 checkpointPos)
    {
        lastCheckpointPosition = checkpointPos;
        Debug.Log("Checkpoint guardado en: " + lastCheckpointPosition);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        lastCheckpointPosition = transform.position; // Guarda la posición inicial como el primer checkpoint
    }

    void Update()
    {
        MovimientoJugador();
        RotacionCamara();
        DetectarBorde();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Saltar();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Atacar();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivarDispositivo();
        }
         if (transform.position.y < -10) // Si el jugador cae del mapa
        {
        ReaparecerEnCheckpoint();
        }
    }

    void MovimientoJugador()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    // Capturar entrada de movimiento en relación con la cámara
        float moveX = Input.GetAxis("Horizontal");  // A y D
        float moveZ = Input.GetAxis("Vertical");    // W y S

    // Dirección de movimiento basada en la cámara
        Vector3 moveDirection = (playerCamera.forward * moveZ + playerCamera.right * moveX).normalized;
        moveDirection.y = 0; // evitar inclinaciones

        if(moveX != 0 || moveZ != 0) animator?.SetFloat("Speed", 1); // Animacion movimiento
        else animator?.SetFloat("Speed", 0);

        controller.Move(moveDirection * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Saltar()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

   void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);

        // Ajustar la rotación vertical de la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Evita que la cámara gire demasiado arriba o abajo

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Atacar()
    {
        animator.SetTrigger("Attack");
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(10);
        }
    }

    void ActivarDispositivo()
    {
        Debug.Log("Dispositivo activado");
    }

    void DetectarBorde()
    {
        RaycastHit hit;
        if (Physics.Raycast(edgeCheck.position, Vector3.down, out hit, edgeGrabDistance))
        {
            if (!isGrounded && hit.collider != null)
            {
                isHanging = true;
                velocity.y = 0;
            }
        }
        else
        {
            isHanging = false;
        }
    }
    


    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Checkpoint alcanzado por: " + other.name);
        other.GetComponent<PlayerController>().GuardarCheckpoint(transform.position);
    }
}
    public void ReaparecerEnCheckpoint()
{
    if (lastCheckpointPosition != Vector3.zero)
    {
        Vector3 spawnPosition = lastCheckpointPosition + Vector3.up * 1.5f; // Eleva el personaje
        controller.enabled = false; // Desactiva el CharacterController para evitar colisiones raras
        transform.position = spawnPosition;
        controller.enabled = true;  // Reactiva el CharacterController
        Debug.Log("Reapareciendo en checkpoint: " + spawnPosition);
    }

        else
        {
            Debug.LogWarning("No hay un checkpoint guardado, reapareciendo en el inicio.");
        }
}
}