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
    public float walkPointrange; //Rango max/min de distancia de generación de puntos de patrulla random
    bool walkPointSet; //Determina si se ha determinado un punto óptimo para ir a patrullar

    [Header("Attack Configuration")]
    public float tiemBetweenAttacks; //Tiempo de espera entre ataques
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
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
