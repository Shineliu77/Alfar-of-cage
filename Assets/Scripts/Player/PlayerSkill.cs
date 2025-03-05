using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float skillRange = 5f;
    public float skillDuration = 5f;
    public float circleExpandTime = 1f; // ����X�i�ɶ�
    public LayerMask enemyLayer;
    public LayerMask GearLayer;
    public GameObject skillCirclePrefab; // ���w�s��

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill();
        }
    }

    void UseSkill()
    {
        // �I��ޯ�S��
        ShowSkillEffect();

        // �d��d�򤺩Ҧ����ĤH
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, skillRange, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            ChasingEnemy enemy = enemyCollider.GetComponent<ChasingEnemy>();
            if (enemy != null)
            {
                enemy.Pause(skillDuration);
            }
        }

        Collider2D[] hitGear = Physics2D.OverlapCircleAll(transform.position, skillRange, GearLayer);
        foreach (Collider2D Gear in hitGear)
        {
            Gear.GetComponent<MovingGround>()?.Pause(skillDuration);
        }
    }

    void ShowSkillEffect()
    {
        if (skillCirclePrefab != null)
        {
            GameObject circle = Instantiate(skillCirclePrefab, transform.position, Quaternion.identity);
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = new Color(0.45f, 0.54f, 0.95f, 0.5f); // 7389F1 �b�z��

            circle.transform.localScale = Vector3.zero;
            circle.transform.DOScale(Vector3.one * skillRange * 2, circleExpandTime)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => Destroy(circle));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}
