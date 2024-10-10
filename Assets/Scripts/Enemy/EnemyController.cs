using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;              // 玩家對象
    public float chaseRange = 5f;         // 追逐範圍
    public float normalSpeed = 2f;        // 普通速度
    public float dashSpeed = 5f;          // 衝刺速度
    public float dashDuration = 0.5f;     // 衝刺持續時間
    public float maxDamage = 10f;         // 最大傷害值
    public float damageImpactMultiplier = 1.5f;  // 速度對傷害的影響倍數

    private float currentSpeed;           // 當前速度
    private bool isDashing = false;       // 是否在衝刺

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 檢查是否在追逐範圍內
        if (distanceToPlayer < chaseRange)
        {
            if (!isDashing)
            {
                StartCoroutine(DashTowardsPlayer());
            }
        }


    }

    private IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            currentSpeed = Mathf.Lerp(normalSpeed, dashSpeed, elapsed / dashDuration);
            elapsed += Time.deltaTime;

            MoveTowardsPlayer(currentSpeed);
            yield return null;
        }

        // 衝刺結束後，計算傷害
        DealDamageToPlayer();
        isDashing = false;
    }

    private void MoveTowardsPlayer(float speed)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void DealDamageToPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    float impactSpeed = Mathf.Clamp(currentSpeed * damageImpactMultiplier, 0, maxDamage);
                    playerHealth.TakeDamage(impactSpeed);  // 對玩家造成傷害
                }
            }
        }
    }




}
