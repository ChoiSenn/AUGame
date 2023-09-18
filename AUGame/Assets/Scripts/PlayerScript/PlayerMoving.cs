using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    float moveX;
    Rigidbody2D rb;

    [Header("AD �̵� �ӵ�")]
    [SerializeField][Range(1000f, 10000f)] float moveSpeed = 3000f;

    [Header("�����̽� ���� ����")]
    [SerializeField][Range(500f, 1000f)] float jumpForce = 700f;

    public Animator animator;  // Animator �ҷ���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // ���� ���̶�� ����
        {

        }
        else  // ���� ���� �ƴ� �� �̵� ����
        {
            // �̵�
            if (Input.GetKey(KeyCode.A)) // A�� ������ ��
            {
                animator.SetBool("Walking", true);  // �Ȱ�����
                animator.SetBool("MoveLeft", true);  // ����

                moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
                rb.velocity = new Vector2(moveX, rb.velocity.y);
            }

            else if (Input.GetKey(KeyCode.D)) // D�� ������ ��
            {
                animator.SetBool("Walking", true);  // �Ȱ�����
                animator.SetBool("MoveLeft", false);  // ������

                moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime; // �������� ���ǵ�� ��Ÿ Ÿ���� ���� ���� x��
                rb.velocity = new Vector2(moveX, rb.velocity.y); // �¿�� �����̴� ���� ����
            }

            else  // a�� d�� ������ ���� ���¶��
            {
                animator.SetBool("Walking", false);  // ����
            }

            if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� ������ ��
            {
                if (rb.velocity.y == 0) // Y�� ���� 0�̸� ����
                {
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);

                    animator.SetBool("Jumping", true);  // ���� ���
                }
            }

            if (rb.velocity.y == 0)  // ���߿� �������� ���� ���
            {
                animator.SetBool("Jumping", false);  // ���� ��� ����
            }
            else
            {
                animator.SetBool("Jumping", true);  // ���� ���
            }

            if (Input.GetMouseButtonDown(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            { // ��Ŭ���� ���Ȱ� Attack �ִϸ��̼��� ����ǰ� ���� �ʴٸ�
                animator.SetTrigger("Attack");  // Attack Ʈ���Ÿ� �����Ͽ� ���� ���
            }
        }
        
        
    }
}