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

    Renderer renderer;
    public GameObject targetEnemy;

    Rigidbody2D rigid;
    public int nextMoveX;  //다음 행동지표를 결정할 변수
    public int nextMoveY;

    BigBatAI BigBatAI;

    void Start()
    {
        renderer = targetEnemy.GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody2D>();

        if (name.Equals("Slime"))  // 슬라임의 경우, 스텟 생성
        {
            hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
            SetEnemyStatus("Slime", 100, 10, 1.5f, 50, 1.5f, 650f);
            nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        }
        else if (name.Equals("Bat"))  // 박쥐의 경우, 스텟 생성
        {
            SetEnemyStatus("Bat", 1, 10, 1.5f, 100, 1.5f, 400f);

            StartCoroutine("BatMoving");  // 박쥐 움직임 반복
        }
        else if (name.Equals("BigBat"))  // 큰 보스 박쥐의 경우, 스텟 생성
        {
            hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
            SetEnemyStatus("BigBat", 1000, 10, 1.5f, 1000000, 1.5f, 400f);
            nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
            // 거대 박쥐 움직임은 따로 스크립트 작성. 나중에 일반 박쥐도 스크립트 분리할것.
        }

        SetAttackSpeed(atkSpeed);
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint
            (new Vector3(transform.position.x, transform.position.y + height, 0));

        if (name.Equals("Slime"))
        {
            hpBar.position = _hpBarPos;
            nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        }

        if (name.Equals("BigBat"))
        {
            nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
        }

        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // 플레이어의 공격이 닿는지 검사
        foreach (Collider2D collider in collider2DsPlayer)
        {
            if (collider.tag == "PlayerAttack")  // 근접 공격에 맞으면
            {
                if (player.attacked)  // 플레이어가 공격 상태면
                {
                    if (name.Equals("Slime") || name.Equals("BigBat"))
                    {
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
                    } else if (name.Equals("Bat"))
                    {
                        Die();
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

            if (name.Equals("Slime") || name.Equals("BigBat"))
            {
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
            else if (name.Equals("Bat"))
            {
                Die();
            }
        }
    }

    public Transform target;

    public GameObject explosion;

    void Attacked()  // 공격당하면 움찔거리는 모션 추가
    {
        enemyAnimator.SetTrigger("Attacked");

        var explo = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explo, 0.5f);

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
        Color c = renderer.material.color;
        c.a = 0.5f;

        if (name.Equals("BigBat"))
        {
            //BigBatAI.Groggy();
            //nowHp = 1000;
        }
        else
        {
            if (name.Equals("Slime"))
            {
                renderer.material.color = c;
                enemyAnimator.SetTrigger("Die");            // die 애니메이션 실행
                GetComponent<EnemySlimeAI>().enabled = false;    // 추적 비활성화
            }
            else
            {
                StopCoroutine("BatMoving");
                var explo = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explo, 0.5f);
                StartCoroutine("FadeOut");
                StartCoroutine("FadeIn");
                StartCoroutine("FadeOut");
                StartCoroutine("FadeIn");
                renderer.material.color = c;
            }
            //Debug.Log("Slime Dying!!");

            GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
            Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화

            Destroy(gameObject, 0.5f);                     // 0.5초후 제거
            if (name.Equals("Slime"))
            {
                Destroy(hpBar.gameObject, 0.5f);               // 1초후 체력바 제거
            }
        }
    }

    void SetAttackSpeed(float speed)
    {
        //enemyAnimator.SetFloat("attackSpeed", speed);
    }

    IEnumerator FadeOut()
    {
        Color c = renderer.material.color;
        c.a = 0.1f;
        renderer.material.color = c;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FadeIn()
    {
        Color c = renderer.material.color;
        c.a = 0.8f;
        renderer.material.color = c;
        yield return new WaitForSeconds(0.1f);
    }

    int count = 0;

    IEnumerator BatMoving()  // 박쥐 몬스터의 일상 움직임
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);  // 초마다 계속 움직이기

            nextMoveX = Random.Range(-1, 2);  // 좌우 다음에 어디로 이동할지 결정
            nextMoveY = Random.Range(-1, 2);  // 상하 다음에 어디로 이동할지 결정
            rigid.velocity = new Vector2(nextMoveX * 100, nextMoveY * 100);
        }
    }
}