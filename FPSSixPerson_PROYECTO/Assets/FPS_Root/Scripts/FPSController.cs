using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    //Private variables (References + value storage)
    CharacterController controller;
    //Cam value storage
    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    //Direction value storage
    Vector2 targetDir;
    Vector2 targetMouseDelta;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;

    #region General Variables
    [Header("General References")]
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    [Header("Cam Parameters")]
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;

    [Header("Movement Parameters")]
    [SerializeField] float speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    [Header("Jump Parameters")]
    [SerializeField] float gravity = -9.8f; //-30f
    public float jumpHeight = 6f;
    [SerializeField] bool isGrounded;
    float velocityY;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked; //Lockea el cursor en el centro de la cam
            Cursor.visible = false; //Hace el cursor invisible al jugador
        }
    }

    // Update is called once per frame
    void Update()
    {
        CamLook();
        Movement();

        //Añadir sensación de peso al caer del salto
        if (!isGrounded && controller.velocity.y < -1f) { velocityY = -8f; }

    }

    void CamLook()
    {
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void Movement()
    {
        //Detección constante del suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        targetDir.Normalize(); //La normalización del vector de dirección hace que el personaje no acelere constantemente
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime); //Emulación de la fricción de un cuerpo físico mediante el uso de "suavizado" entre dos vectores
        velocityY -= gravity * -2f * Time.deltaTime; //Hace que al personaje le afecte la gravedad "custom" que necesitamos generar al usar el componente Character Controller

        //Dos operaciones de movimiento:
        //Acción 1: Crear una información vectorial constante que "emule" el velocity de un RigidBody, se le tiene que aplicar la gravedad custom en el eje y
        //Acción 2: Le paso al Character Controller dicha velocidad mediante un método de librería interna (Move()) multiplicada por el delta time porque ejecuta en Update
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

    }

    #region Input Events
    public void OnLook(InputAction.CallbackContext context)
    {
        targetMouseDelta = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        targetDir = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    #endregion
}
