using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractor : MonoBehaviour
{
    [Header("Enemy General Parameters")]
    [SerializeField] int enemyHitPoints;
    [SerializeField] int enemyMaxHP;

    [Header("Feedback References")]
    MeshRenderer enemyRend;
    [SerializeField] Material baseMat;
    [SerializeField] Material damagedMat;
    [SerializeField] float feedbackResetTime = 0.1f;

    private void Start()
    {
        enemyRend = GetComponent<MeshRenderer>();
        enemyHitPoints = enemyMaxHP;
    }

    private void Update()
    {
        if ( enemyHitPoints <= 0) { gameObject.SetActive(false); }
    }

    public void TakeDamage(int damage)
    {
        enemyRend.material = damagedMat;
        enemyHitPoints -= damage;
        Invoke(nameof(ResetDamageFeedback), feedbackResetTime);
    }

    void ResetDamageFeedback()
    {
        enemyRend.material = baseMat;
    }
}
