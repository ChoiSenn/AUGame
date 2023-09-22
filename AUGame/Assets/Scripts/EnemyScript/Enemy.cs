using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    public float height = 1.7f;

    public PlayerMoving player;
    Image nowHpbar;
    public Animator enemyAnimator;

    void Start()
    {
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if (name.Equals("Slime"))  // 슬라임의 경우, 스텟 생성
        {
            SetEnemyStatus("Slime", 100, 10, 1.5f, 50, 1.5f, 650f);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();

        SetAttackSpeed(atkSpeed);
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
            (new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }
    private void OnTriggerEnter2D(Collider2D col)  // 해당 태그에 닿았을 때
    {
        if (col.CompareTag("PlayerAttack"))
        {
            if (player.attacked)
            {
                //nowHp -= player.atkDmg;
                nowHp -= 10;
                //player.attacked = false;

                if (nowHp <= 0) // 적 사망
                {
                    Die();
                }
                else
                {
                    Attacked();  // 공격 받는 모션
                }
            }
        }
    }

    public Transform target;

    void Attacked()  // 공격당하면 움찔거리는 모션 추가
    {
        enemyAnimator.SetTrigger("Attacked");

        float dir = target.position.x - transform.position.x;
        if(dir < 0)  // 적과 플레이어 위치에 따라 뒤로 20만큼 넉백 줌
        {
            transform.Translate(new Vector2(20, 0) * 100 * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(-20, 0) * 100 * Time.deltaTime);
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("Die");            // die 애니메이션 실행
        Debug.Log("Slime Dying!!");
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
        Destroy(gameObject, 1);                     // 3초후 제거
        Destroy(hpBar.gameObject, 1);               // 3초후 체력바 제거
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}