using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("AD �̵� �ӵ�")]
    [SerializeField][Range(0, 1000)] int speed = 150;

    [Header("�����̽� ���� ����")]
    [SerializeField][Range(0f, 100f)] float jumpForce = 3f;

    public Animator animator;  // Animator �ҷ���

    public Transform pos;
    public Vector2 boxSize;

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

                transform.position = transform.position - transform.right * Time.deltaTime * speed;
                //moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
                //rb.velocity = new Vector2(moveX, rb.velocity.y);
            }

            else if (Input.GetKey(KeyCode.D)) // D�� ������ ��
            {
                animator.SetBool("Walking", true);  // �Ȱ�����
                animator.SetBool("MoveLeft", false);  // ������

                transform.position = transform.position + transform.right * Time.deltaTime * speed;
                //moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime; // �������� ���ǵ�� ��Ÿ Ÿ���� ���� ���� x��
                //rb.velocity = new Vector2(moveX, rb.velocity.y); // �¿�� �����̴� ���� ����
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

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // �浹 �˻�
                foreach (Collider2D collider in collider2Ds)
                {
                    //Debug.Log(collider.tag);
                }
            }
        }
    }

    public bool attacked = false;

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    private void OnDrawGizmos()  // �ǰ� ���� �׸��� 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}