using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class fireMagic : MonoBehaviour
{
    public float speed; //���ǵ� ����
    public float distance;
    public LayerMask isLayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyFire", 2); // ���� �ð� �� �ı� = Invoke("���� �� �Լ���", ���� �ð�)
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (ray.collider != null)
        {
            if (ray.collider.tag == "Enemy") // �±� Enemy ����, �����ӿ��� �Ҵ� �� ���.
            {
                Debug.Log("����!");

            }
            DestroyFire();
        }

        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // ���������� ������ fire Ŭ�� �߻�
        }
        else 
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime); // �������� ������ fire Ŭ�� �߻�
        }
    }

    void DestroyFire()
    {
        //Destroy(gameObject); // Destroy(�ı��� ������Ʈ)
    }
}