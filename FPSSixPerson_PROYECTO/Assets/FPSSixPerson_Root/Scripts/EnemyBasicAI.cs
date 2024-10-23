using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasicAI : MonoBehaviour
{
    #region General Variables
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent; //Ref al componente de IA
    [SerializeField] Transform target; //Ref al transform del objetivo a perseguir
    [SerializeField] LayerMask targetLayer; //Capa del objetivo, para detectarlo
    [SerializeField] LayerMask groundLayer; //Capa para detectar el suelo. Si no detecta suelo, no patrulla

    [Header("patroling Stats")]
    public Vector3 walkPoint; //Posición a la que ir cuando está en modo patrulla random
    public float walkPointRange; //Rango max/min de distancia de generación de puntos de patrulla random
    bool walkPointSet; //Determina si se ha determinado un punto óptimo para ir a patrullar

    [Header("Attack Configuration")]
    public float timeBetweenAttacks; //Tiempo de espera entre ataques
    bool alreadyAttacked; //Define si el personaje está atacando , para no atacar infinito
    [SerializeField] GameObject projectile; //Ref al prefab del proyectil
    [SerializeField] Transform shootPoint; //punto del que saldra el proyectil
    [SerializeField] float projectileSpeed; //Velocidad del proyectil

    [Header("States & Detectipon")]
    [SerializeField] float sightRange; //Rango de detección PERSEGUIR
    [SerializeField] float attackRange; //Rango de detección ATACAR
    [SerializeField] bool targetInSightRange; //TRUE cuando pasamos a PERSEGUIR
    [SerializeField] bool targetInAttackRange; //TRUE cuendo pasamos a ATACAR

    #endregion

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Los bools de estados verdaderos cuando detecta al player DENTRO del redio definido
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);

        //Máquina de estados con booleanos: lógica del comportamiento del agente
        if (!targetInSightRange && !targetInAttackRange) { Patroling(); }
        if (targetInSightRange && !targetInAttackRange) { ChaseTarget(); }
        if (targetInSightRange && targetInAttackRange) { AttackTarget(); }
    }

    void Patroling()
    {
        //Condicional que define si el agente tiene que encontrar un nuevo punto al que ir o perseguir un punto ya creado
        if (!walkPointSet) { SearchWalkPoint(); }
        else { agent.SetDestination(walkPoint); }

        //variable que calcula constantemente la distancia entre el agente y el punto a alcanzar
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1) { walkPointSet = false; }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        //Almacena una nueva posici´pon vectorial basada en la posición ACTUAL del agente más los valores random en X/Z
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z +randomZ);
        //Si detecta el suelo desde el nuevo punto, le dice al agente que vaya a ese punto
        //Si no, vuelve a ejecutar SearchWalkPoint()
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    void ChaseTarget()
    {
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        //Le decimos al agente que se persiga a si mismo
        agent.SetDestination(transform.position);
        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            //Código de ataque: es variable al gusto del progrador
            //Este atque solo es un ejemplo: disparo de bala física
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
