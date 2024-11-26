using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float skillRange = 5f;
    public float skillDuration = 5f;
    public LayerMask enemyLayer;
    public LayerMask GearLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill();
        }
    }

    void UseSkill()
    {
        // 查找範圍內所有的敵人
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, skillRange, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // 使敵人暫停
            ChasingEnemy enemy = enemyCollider.GetComponent<ChasingEnemy>();
            if (enemy != null)
            {
                enemy.Pause(skillDuration);  // 調用敵人腳本的暫停方法
            }
        }

        // 查找範圍內的Gear（可選的功能）
        Collider2D[] hitGear = Physics2D.OverlapCircleAll(transform.position, skillRange, GearLayer);
        foreach (Collider2D Gear in hitGear)
        {
            Gear.GetComponent<MovingGround>()?.Pause(skillDuration);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }
}
