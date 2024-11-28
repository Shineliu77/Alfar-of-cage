using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
  
        public GameObject vinePrefab;  // 藤蔓的Prefab
        public float attackInterval = 2f;  // 施放藤蔓的間隔
        public float yOffset = -2f; // BOSS 往下移動的偏移量
        private bool isPaused = false;  // 判斷 BOSS 是否被暫停

        private void Start()
        {
            StartCoroutine(AttackVinesRoutine());
        }

        // 開始藤蔓攻擊循環
        private IEnumerator AttackVinesRoutine()
        {
            while (true)
            {
                if (!isPaused)
                {
                    // 施放藤蔓攻擊
                    LaunchVineAttack();

                    // 每次施放完藤蔓後，等待一定時間
                    yield return new WaitForSeconds(attackInterval);
                }
                yield return null;
            }
        }

        // 施放藤蔓攻擊
        private void LaunchVineAttack()
        {
            // 當 BOSS 施放藤蔓攻擊時，讓 BOSS 往下移動
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
            StartCoroutine(MoveBossToPosition(targetPosition));

            // 根據施放順序確定藤蔓的方向
            // 第一次施放藤蔓往左，第二次往右，第三次兩邊都施放
            if (Random.Range(0, 2) == 0)
            {
                // 向左施放藤蔓
                Instantiate(vinePrefab, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else
            {
                // 向右施放藤蔓
                Instantiate(vinePrefab, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }

        // 讓 BOSS 移動到指定位置
        private IEnumerator MoveBossToPosition(Vector3 targetPosition)
        {
            float moveSpeed = 2f;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // 控制 BOSS 暫停
        public void Pause(float duration)
        {
            StartCoroutine(PauseRoutine(duration));
        }

        private IEnumerator PauseRoutine(float duration)
        {
            isPaused = true;
            yield return new WaitForSeconds(duration);
            isPaused = false;
        }
    }


