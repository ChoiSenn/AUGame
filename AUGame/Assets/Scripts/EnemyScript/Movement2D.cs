using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private float moveLowSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))  // ����Ʈ Ű�� ���� �̵�
        {
            transform.position += moveDirection * moveLowSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void MoveTo(Vector3 direction)  // �ܺο��� MoveTo �Լ� ȣ���Ͽ� ����
    {
        moveDirection = direction;
    }
}