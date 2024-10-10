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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, skillRange, enemyLayer);


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

    
