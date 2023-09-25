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

    public Transform pos;
    public Vector2 boxSize;

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

        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // 플레이어의 공격이 닿는지 검사
        foreach (Collider2D collider in collider2DsPlayer)
        {
            if (collider.tag == "PlayerAttack")  // 근접 공격에 맞으면
            {
                if (player.attacked)  // 플레이어가 공격 상태면
                {
                    //nowHp -= player.atkDmg;
                    nowHp -= 10;  // 데미지 받고
                    player.attacked = false;

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
    }

    private void OnTriggerEnter2D(Collider2D col)  // 해당 태그에 닿았을 때 
    {
        if (col.CompareTag("PlayerMagicAttack"))
        {  // 마법 발사체에 맞으면
            Destroy(col.gameObject);  // 맞은 발사체 제거

            nowHp -= 30;
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

    public Transform target;

    void Attacked()  // 공격당하면 움찔거리는 모션 추가
    {
        enemyAnimator.SetTrigger("Attacked");

        float dir = target.position.x - transform.position.x;
        if(dir < 0)  // 적과 플레이어 위치에 따라 뒤로 넉백 줌
        {
            transform.Translate(new Vector2(30, 0) * 100 * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(-30, 0) * 100 * Time.deltaTime);
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("Die");            // die 애니메이션 실행
        Debug.Log("Slime Dying!!");
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
        Destroy(gameObject, 1);                     // 1초후 제거
        Destroy(hpBar.gameObject, 1);               // 1초후 체력바 제거
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}