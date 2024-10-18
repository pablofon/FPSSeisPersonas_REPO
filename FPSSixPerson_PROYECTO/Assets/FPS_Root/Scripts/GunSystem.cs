using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //Ref a la c�mara (Si disparamos desde c�mara)
    [SerializeField] Transform shootPoint; //Ref al empty desde el que se dispara (Si disparamos desde un punto concreto)
    [SerializeField] RaycastHit hit; //Almac�n de la informaci�n de choque de los disparos
    //Declaraci�n de layers contras las que S� chocar� nuestro disparo
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] AudioSource weaponAudio;

    [Header("Weapon Stats")]
    public int damage; //Da�o base del arma (por rayo impactado)
    public float range; //Longitud del rayo (Distancia m�xima de tiro)
    public float spread; //Dispersi�n del arma
    public float shootingCooldown; //Cadencia de tiro (delay entre input)
    public float timeBetweenShoots; //Extra cadencia (en caso de que el arma sea autom�tica o continua)
    public float reloadTime; //Tiempo de recarga
    public bool allowButtonHold; //Si el disparo es por tap input o por hold input

    [Header("Bullet Management")]
    public int ammoSize; //Cantidad de balas m�xima
    public int bulletsPerTap; //Cu�ntas balas se disparan por rayo
    [SerializeField] int bulletsLeft; //Cu�ntas balas quedan dentro del cargador
    [SerializeField] int bulletsShot; //Cu�ntas balas hemos disparado

    [Header("State Bools")]
    [SerializeField] bool shooting; //Estamos en el proceso de disparo
    [SerializeField] bool canShoot; //Define si es posible disparar o no
    [SerializeField] bool reloading; //Estamos en el proceso de recarga

    [Header("Graphics")]
    [SerializeField] GameObject muzzleFlash; //Referencia al FX o luz del ca��n cuando dispara
    [SerializeField] GameObject hitGraphic; //Referencia al "gr�fico" de impacto de las balas

    [Header("Sounf System")]
    [SerializeField] AudioClip[] weaponSoundLibrary;

    #endregion

    private void Awake()
    {
        weaponAudio = GetComponent<AudioSource>();

        bulletsLeft = ammoSize;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        canShoot = false; //No podemos disparar porque YA ESTAMOS disparando

        //Opcional: Dispersi�n variable
        float spreadX = Random.Range(-spread, spread);
        float spreadY = Random.Range(-spread, spread);
        float spreadZ = Random.Range(-spread, spread);
        Vector3 vectorSpread = new Vector3(spreadX, spreadY, spreadZ);
        //Almacenar la direcci�n de disparo: Vector3 hacia adelante + nuevo vector de dispersi�n
        Vector3 direction = fpsCam.transform.forward + vectorSpread;

        //RAYCAST DEL DISPARO
        //Physics.Raycast(Origen del rayo, Direcci�n del rayo, Variable almac�n del rayo, Longitud del rayo, Layer a la que golpea el rayo)
        //Si se desea un rayo infinito, en la longitud se declara Mathf.Infinity
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, interactableLayer))
        {
            Debug.Log(hit.collider.name); //Me indica el nombre del objeto con el que el rayo ha colisionado
            if (hit.collider.CompareTag("Enemy"))
            {
                //HACER DA�O
                EnemyInteractor enemyDamageScript = hit.collider.GetComponent<EnemyInteractor>();
                enemyDamageScript.TakeDamage(damage);
            }

            

        }
        //Gr�ficos del disparo
        muzzleFlash.SetActive(true);

        bulletsLeft--;
        bulletsShot++;

        //Resetear el disparo
        if (!IsInvoking(nameof(ResetShoot)) && !canShoot) Invoke(nameof(ResetShoot), shootingCooldown);

    }

    void ResetShoot()
    {
        muzzleFlash.SetActive(false);
        canShoot = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
        //El valor de reloadTime deber�a ser igual a la duraci�n de la animaci�n de recarga
    }

    void ReloadFinished()
    {
        bulletsLeft = ammoSize;
        reloading = false;
    }

    #region New Input Events
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && canShoot)
        {
            Debug.Log("Hola");
            if (!reloading && bulletsLeft > 0)
            {
                weaponAudio.PlayOneShot(weaponSoundLibrary[0]);
                Shoot();
            }
            else
            {
                //Sonido de no tengo balas
            }
        }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (bulletsLeft < ammoSize && !reloading) 
            {
                weaponAudio.PlayOneShot(weaponSoundLibrary[1]);
                Reload(); 
            }
            else Debug.Log("Tienes el cargador a tope!");
        }
    }
    #endregion
}
