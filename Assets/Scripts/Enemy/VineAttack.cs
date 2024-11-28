using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAttack : MonoBehaviour
{
  
        public GameObject vinePrefab;  // �ý���Prefab
        public float attackInterval = 2f;  // �I���ý������j
        public float yOffset = -2f; // BOSS ���U���ʪ������q
        private bool isPaused = false;  // �P�_ BOSS �O�_�Q�Ȱ�

        private void Start()
        {
            StartCoroutine(AttackVinesRoutine());
        }

        // �}�l�ý������`��
        private IEnumerator AttackVinesRoutine()
        {
            while (true)
            {
                if (!isPaused)
                {
                    // �I���ý�����
                    LaunchVineAttack();

                    // �C���I���ý���A���ݤ@�w�ɶ�
                    yield return new WaitForSeconds(attackInterval);
                }
                yield return null;
            }
        }

        // �I���ý�����
        private void LaunchVineAttack()
        {
            // �� BOSS �I���ý������ɡA�� BOSS ���U����
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
            StartCoroutine(MoveBossToPosition(targetPosition));

            // �ھڬI�񶶧ǽT�w�ý�����V
            // �Ĥ@���I���ý������A�ĤG�����k�A�ĤT�����䳣�I��
            if (Random.Range(0, 2) == 0)
            {
                // �V���I���ý�
                Instantiate(vinePrefab, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else
            {
                // �V�k�I���ý�
                Instantiate(vinePrefab, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }

        // �� BOSS ���ʨ���w��m
        private IEnumerator MoveBossToPosition(Vector3 targetPosition)
        {
            float moveSpeed = 2f;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // ���� BOSS �Ȱ�
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


