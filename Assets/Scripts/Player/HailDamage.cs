using UnityEngine;

public class HailDamage : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ˬd�O�_�I���� BOSS
        if (collision.gameObject.CompareTag("Boss"))
        {
            // �� BOSS ���ˡA�ñN damage �j���ഫ�� int
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage((int)damage); // �N damage �ഫ�� int
            }

            // ������P���B�r
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // ��B�r�I��a���ɾP���B�r
            Destroy(gameObject);
        }
    }
}
