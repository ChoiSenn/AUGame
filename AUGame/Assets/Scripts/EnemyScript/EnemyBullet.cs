using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // �߻�ü�� �ε��� ������Ʈ�� �±װ� Player���
        {
            Destroy(gameObject);  // �߻�ü ������Ʈ ����
        }
    }
}