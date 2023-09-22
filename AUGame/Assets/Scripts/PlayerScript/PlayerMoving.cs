using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("AD 이동 속도")]
    [SerializeField][Range(0, 1000)] int speed = 150;

    [Header("스페이스 점프 강도")]
    [SerializeField][Range(0f, 100f)] float jumpForce = 3f;

    public Animator animator;  // Animator 불러옴

    public Transform pos;
    public Vector2 boxSize;

    public Transform posPlayer;

    public string magic = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))  // 공격 중이라면 멈춤
        {

        }
        else  // 공격 중이 아닐 때 이동 가능
        {
            if (Input.GetKey(KeyCode.E)) // 마법 테스트용 코드!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                magic = "fire";
            } else if (Input.GetKey(KeyCode.Q))
            {
                magic = null;
            }

            // 이동
            if (Input.GetKey(KeyCode.A)) // A를 눌렀을 때
            {
                animator.SetBool("Walking", true);  // 걷고있음
                animator.SetBool("MoveLeft", true);  // 왼쪽
                transform.eulerAngles = new Vector3(0, 180, 0);

                transform.position = transform.position + transform.right * Time.deltaTime * speed;
            }

            else if (Input.GetKey(KeyCode.D)) // D를 눌렀을 때
            {
                animator.SetBool("Walking", true);  // 걷고있음
                animator.SetBool("MoveLeft", false);  // 오른쪽
                transform.eulerAngles = new Vector3(0, 0, 0);

                transform.position = transform.position + transform.right * Time.deltaTime * speed;
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

                    animator.SetBool("Jumping", true);  // 점프 모션
                }
            }

            if (rb.velocity.y == 0)  // 공중에 떠있으면 점프 모션
            {
                animator.SetBool("Jumping", false);  // 점프 모션 해제
            }
            else
            {
                animator.SetBool("Jumping", true);  // 점프 모션
            }

            if (Input.GetMouseButtonDown(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            { // 좌클릭이 눌렸고 Attack 애니메이션이 실행되고 있지 않다면
                animator.SetTrigger("Attack");  // Attack 트리거를 설정하여 공격 모션

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // 충돌 검사
                foreach (Collider2D collider in collider2Ds)
                {
                    //Debug.Log(collider.tag);
                }
            }

            Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // 적에 닿는 충돌 검사
            foreach (Collider2D collider in collider2DsPlayer)
            {
                if (collider.tag == "Enemy")
                {
                    Die();
                }
            }
        }
    }

    public bool attacked = false;

    void AttackTrue()  // Enemy에서 공격당하고 있는지 여부를 판단
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    private void OnDrawGizmos()  // 피격 범위 그리기  (지워도 되는 코드)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    void Die()  // 해당 태그에 닿았을 때
    {
        transform.position = new Vector3(-315.7418f, -58.85741f, 599.7907f);  // 처음에 있던 위치로 (게임오버 개념. 게임오버 추가할 것)
    }
}