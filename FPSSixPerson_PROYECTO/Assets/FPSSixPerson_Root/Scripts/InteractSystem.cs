using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractSystem : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Ref a la cámara (Si disparamos desde cámara)
    [SerializeField] Transform shootPoint; //Ref al empty desde el que se dispara (Si disparamos desde un punto concreto)
    [SerializeField] RaycastHit hit; //Almacén de la información de choque de los disparos
    //Declaración de layers contras las que SÍ chocará nuestro disparo
    [SerializeField] LayerMask interactableLayer;

    [Header("Interact Stats")]
    //public int damage; //Daño base del arma (por rayo impactado)
    public float range; //Longitud del rayo (Distancia máxima de tiro)
    public float spread; //Dispersión del arma
    //public float shootingCooldown; //Cadencia de tiro (delay entre input)
    //public float timeBetweenShoots; //Extra cadencia (en caso de que el arma sea automática o continua)
    //public float reloadTime; //Tiempo de recarga
    public bool allowButtonHold; //Si el disparo es por tap input o por hold input
    [SerializeField] bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Interact()
    {
        canInteract = false;

        float spreadX = Random.Range(-spread, spread);
        float spreadY = Random.Range(-spread, spread);
        float spreadZ = Random.Range(-spread, spread);
        Vector3 vectorSpread = new Vector3(spreadX, spreadY, spreadZ);
        //Almacenar la dirección de disparo: Vector3 hacia adelante + nuevo vector de dispersión
        Vector3 direction = fpsCam.transform.forward + vectorSpread;

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, interactableLayer))
        {
            Debug.Log(hit.collider.name); //Me indica el nombre del objeto con el que el rayo ha colisionado
            if (hit.collider.CompareTag("FirstKey")) //Si coge una llave
            {
                KeyInteractor keyGrabScript = hit.collider.GetComponent<KeyInteractor>();
                keyGrabScript.Grabbed();
            }

            if (hit.collider.CompareTag("LockedDoor"))
            {
                LockedDoorInteractor unlockDoorScript = hit.collider.GetComponent<LockedDoorInteractor>();
                unlockDoorScript.Unlocked();
            }

            if (hit.collider.CompareTag("Door"))
            {
                DoorInteractor openDoorScript = hit.collider.GetComponent<DoorInteractor>();
                openDoorScript.Open();
            }

            if (hit.collider.CompareTag("CodeDoor"))
            {
                GameManager.Instance.usingKeypad = true;
            }
        }

        if (!IsInvoking(nameof(ResetInteract)) && !canInteract) Invoke(nameof(ResetInteract), 0.01f);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Interact();
        }
    }

    void ResetInteract()
    {
        canInteract = true;
    }
}
