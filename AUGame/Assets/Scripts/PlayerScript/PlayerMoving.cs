using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    float moveX;
    Rigidbody2D rb;

    [Header("AD 이동 속도")]
    [SerializeField][Range(1000f, 10000f)] float moveSpeed = 5000f;

    [Header("스페이스 점프 강도")]
    [SerializeField][Range(500f, 1000f)] float jumpForce = 700f;

    public Animator animator;  // Animator 불러옴

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 이동
        if (Input.GetKey(KeyCode.A)) // A를 눌렀을 때
        {
            animator.SetBool("Walking", true);  // 걷고있음
            animator.SetBool("MoveLeft", true);  // 왼쪽

            moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime;
            rb.velocity = new Vector2(moveX, rb.velocity.y);
        }

        else if (Input.GetKey(KeyCode.D)) // D를 눌렀을 때
        {
            animator.SetBool("Walking", true);  // 걷고있음
            animator.SetBool("MoveLeft", false);  // 오른쪽

            moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.smoothDeltaTime; // 수평으로 스피드와 델타 타입을 곱한 값을 x에
            rb.velocity = new Vector2(moveX, rb.velocity.y); // 좌우로 움직이는 값을 저장
        }

        else  // a도 d도 눌리지 않은 상태라면
        {
            animator.SetBool("Walking", false);  // 멈춤
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스를 눌렀을 때
        {
            if (rb.velocity.y == 0) // Y의 값이 0이면 점프
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }
        }
    }
}