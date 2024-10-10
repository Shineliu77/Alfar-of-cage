using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;              // ���a��H
    public float chaseRange = 5f;         // �l�v�d��
    public float normalSpeed = 2f;        // ���q�t��
    public float dashSpeed = 5f;          // �Ĩ�t��
    public float dashDuration = 0.5f;     // �Ĩ����ɶ�
    public float maxDamage = 10f;         // �̤j�ˮ`��
    public float damageImpactMultiplier = 1.5f;  // �t�׹�ˮ`���v�T����

    private float currentSpeed;           // ��e�t��
    private bool isDashing = false;       // �O�_�b�Ĩ�

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // �ˬd�O�_�b�l�v�d��
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

        // �Ĩ뵲����A�p��ˮ`
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
                    playerHealth.TakeDamage(impactSpeed);  // �缾�a�y���ˮ`
                }
            }
        }
    }




}
