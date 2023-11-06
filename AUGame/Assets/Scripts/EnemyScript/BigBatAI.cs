using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBatAI : MonoBehaviour
{
    public Transform target;
    public GameObject targetEnemy;

    Enemy enemy;
    Animator enemyAnimator;
    Rigidbody2D rigid;

    [SerializeField]
    private GameObject bulletSmall;  // 공격할 때 생성되는 발사체 프리팹
    [SerializeField]
    private GameObject bulletMedium;
    [SerializeField]
    private GameObject bulletLarge;

    Renderer renderer;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
        rigid = GetComponent<Rigidbody2D>();
        renderer = targetEnemy.GetComponent<Renderer>();

        StartCoroutine("Bullet1Pattern");  // 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.nowHp < 0)
        {
            rigid.velocity = new Vector2(0 * 100, 0 * 100);

            StopCoroutine("MoveLeftPattern");
            StopCoroutine("MoveRightPattern");
            StopCoroutine("PrepareLeftPattern");
            StopCoroutine("PrepareRightPattern");
            StopCoroutine("RushLeftPattern");
            StopCoroutine("RushRightPattern");
            StopCoroutine("SwingLeftPattern");
            StopCoroutine("SwingRightPattern");
            StopCoroutine("Bullet1Pattern");
            StopCoroutine("MoveDownPattern");
            StopCoroutine("Bullet2Pattern");
            StopCoroutine("BatMoving");

            groggyPrepare();
        }
    }

    void groggyPrepare()
    {
        StartCoroutine("Groggy");
    }

    public IEnumerator Groggy()  // 체력이 다 닳으면 그로기 상태
    {
        yield return new WaitForSeconds(5.0f);

        enemy.nowHp = 1000;

        StopCoroutine("MoveLeftPattern");
        StopCoroutine("MoveRightPattern");
        StopCoroutine("PrepareLeftPattern");
        StopCoroutine("PrepareRightPattern");
        StopCoroutine("RushLeftPattern");
        StopCoroutine("RushRightPattern");
        StopCoroutine("SwingLeftPattern");
        StopCoroutine("SwingRightPattern");
        StopCoroutine("Bullet1Pattern");
        StopCoroutine("MoveDownPattern");
        StopCoroutine("Bullet2Pattern");
        StopCoroutine("BatMoving");

        StartCoroutine("Bullet1Pattern");
    }

    IEnumerator MoveLeftPattern()  // 왼쪽으로 천천히 이동
    {
        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 200, 0 * 200);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine("PrepareRightPattern");
    }

    IEnumerator MoveRightPattern()  // 오른쪽으로 천천히 이동
    {
        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 100, 0 * 100);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine("MoveLeftPattern");
    }

    IEnumerator PrepareLeftPattern()  // 돌진 전 준비
    {
        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(-1 * 100, -1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(-1 * 100, -1 * 100);

        StartCoroutine("RushLeftPattern");
    }

    IEnumerator PrepareRightPattern()  // 돌진 전 준비
    {

        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(-1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(1 * 100, -1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(-1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(1 * 100, -1 * 100);

        StartCoroutine("RushRightPattern");
    }

    IEnumerator RushLeftPattern()  // 왼쪽으로 돌진
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 500, 0 * 500);

        yield return new WaitForSeconds(2.0f);

        StartCoroutine("Bullet2Pattern");
    }
    IEnumerator RushRightPattern()  // 오른쪽으로 돌진
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 500, 0 * 500);

        yield return new WaitForSeconds(2.0f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine("SwingLeftPattern");
    }

    IEnumerator SwingLeftPattern()  // 왼쪽 포물선 돌진
    {
        transform.localScale = new Vector3(200, 200, 1);

        rigid.velocity = new Vector2(-1 * 500, -0.8f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.7f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.6f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.5f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.4f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.3f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.2f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, -0.1f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.1f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.2f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.3f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.4f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.5f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.6f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.7f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(-1 * 500, 0.8f * 500);
        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("SwingRightPattern");
    }

    IEnumerator SwingRightPattern()  // 오른쪽 포물선 돌진
    {

        transform.localScale = new Vector3(-200, 200, 1);

        rigid.velocity = new Vector2(1 * 500, -0.8f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.7f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.6f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.5f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.4f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.3f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.2f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, -0.1f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.1f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.2f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.3f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.4f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.5f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.6f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.7f * 500);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 500, 0.8f * 500);
        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("MoveDownPattern");
    }

    IEnumerator Bullet1Pattern()  // 탄막 발사 패턴 1
    {
        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.0f);

        transform.localScale = new Vector3(200, 200, 1);

        float attackRate = 1.0f;  // 공격 주기
        int bulletCount = 10;  // 발사체 생성 개수
        float intervalAngle = 360 / bulletCount;  // 발사체 사이의 각도
        float weightAngle = 0;  // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        int count = 0;

        while (count < 5)  // 5번 동안 count 개수만큼 원 형태로 방사하는 발사체 생성
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletSmall, transform.position, Quaternion.identity);  // 발사체 생성
                float angle = weightAngle + intervalAngle * i;  // 발사체 이동 방향(각도)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // 발사체 이동 방향 결정
            }

            weightAngle += 5;  // 발사체가 생성되는 시작 각도 설정을 위한 변수

            count++;
            yield return new WaitForSeconds(attackRate);  // attackRate 시간 만큼 대기
        }


        StartCoroutine("MoveLeftPattern");
    }

    IEnumerator MoveDownPattern()  // 아래로 조금 이동
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(0 * 500, -1 * 200);

        yield return new WaitForSeconds(1.0f);

        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine("PrepareLeftPattern");
    }

    IEnumerator Bullet2Pattern()  // 탄막 발사 패턴 2
    {
        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 0, 1 * 200);
        yield return new WaitForSeconds(1.0f);
        rigid.velocity = new Vector2(1 * 0, 1 * 0);

        float attackRate = 0.8f;  // 공격 주기
        int AbulletCount = 8;  // 발사체 생성 개수
        float AintervalAngle = 360 / AbulletCount;  // 발사체 사이의 각도
        int BbulletCount = 8;  // 발사체 생성 개수
        float BintervalAngle = 360 / AbulletCount;  // 발사체 사이의 각도
        float weightAngle = 0;  // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        int count = 0;

        while (count < 10)  // 10번 동안 count 개수만큼 원 형태로 방사하는 발사체 생성
        {
            for (int i = 0; i < AbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletMedium, transform.position, Quaternion.identity);  // 발사체 생성
                float angle = weightAngle + AintervalAngle * i;  // 발사체 이동 방향(각도)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // 발사체 이동 방향 결정
            }

            for (int i = 0; i < BbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletLarge, transform.position, Quaternion.identity);  // 발사체 생성
                float angle = weightAngle + 3 + BintervalAngle * i;  // 발사체 이동 방향(각도)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // 발사체 이동 방향 결정
            }

            for (int i = 0; i < AbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletMedium, transform.position, Quaternion.identity);  // 발사체 생성
                float angle = weightAngle + 6 + AintervalAngle * i;  // 발사체 이동 방향(각도)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(각도), 라디안 각도의 각도 표현을 위해 PI / 180을 곱함
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // 발사체 이동 방향 결정
            }

            weightAngle += 10;  // 발사체가 생성되는 시작 각도 설정을 위한 변수

            count++;
            yield return new WaitForSeconds(attackRate);  // attackRate 시간 만큼 대기
        }


        StartCoroutine("MoveRightPattern");
    }

}
