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
    private GameObject bulletSmall;  // ������ �� �����Ǵ� �߻�ü ������
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

        StartCoroutine("Bullet1Pattern");  // �ڷ�ƾ ����
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

    public IEnumerator Groggy()  // ü���� �� ������ �׷α� ����
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

    IEnumerator MoveLeftPattern()  // �������� õõ�� �̵�
    {
        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 200, 0 * 200);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine("PrepareRightPattern");
    }

    IEnumerator MoveRightPattern()  // ���������� õõ�� �̵�
    {
        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 100, 0 * 100);

        yield return new WaitForSeconds(3.0f);

        StartCoroutine("MoveLeftPattern");
    }

    IEnumerator PrepareLeftPattern()  // ���� �� �غ�
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

    IEnumerator PrepareRightPattern()  // ���� �� �غ�
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

    IEnumerator RushLeftPattern()  // �������� ����
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 500, 0 * 500);

        yield return new WaitForSeconds(2.0f);

        StartCoroutine("Bullet2Pattern");
    }
    IEnumerator RushRightPattern()  // ���������� ����
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

    IEnumerator SwingLeftPattern()  // ���� ������ ����
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

    IEnumerator SwingRightPattern()  // ������ ������ ����
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

    IEnumerator Bullet1Pattern()  // ź�� �߻� ���� 1
    {
        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.0f);

        transform.localScale = new Vector3(200, 200, 1);

        float attackRate = 1.0f;  // ���� �ֱ�
        int bulletCount = 10;  // �߻�ü ���� ����
        float intervalAngle = 360 / bulletCount;  // �߻�ü ������ ����
        float weightAngle = 0;  // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        int count = 0;

        while (count < 5)  // 5�� ���� count ������ŭ �� ���·� ����ϴ� �߻�ü ����
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletSmall, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + intervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            weightAngle += 5;  // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����

            count++;
            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }


        StartCoroutine("MoveLeftPattern");
    }

    IEnumerator MoveDownPattern()  // �Ʒ��� ���� �̵�
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(0 * 500, -1 * 200);

        yield return new WaitForSeconds(1.0f);

        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine("PrepareLeftPattern");
    }

    IEnumerator Bullet2Pattern()  // ź�� �߻� ���� 2
    {
        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 0, 1 * 200);
        yield return new WaitForSeconds(1.0f);
        rigid.velocity = new Vector2(1 * 0, 1 * 0);

        float attackRate = 0.8f;  // ���� �ֱ�
        int AbulletCount = 8;  // �߻�ü ���� ����
        float AintervalAngle = 360 / AbulletCount;  // �߻�ü ������ ����
        int BbulletCount = 8;  // �߻�ü ���� ����
        float BintervalAngle = 360 / AbulletCount;  // �߻�ü ������ ����
        float weightAngle = 0;  // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        int count = 0;

        while (count < 10)  // 10�� ���� count ������ŭ �� ���·� ����ϴ� �߻�ü ����
        {
            for (int i = 0; i < AbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletMedium, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + AintervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            for (int i = 0; i < BbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletLarge, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + 3 + BintervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            for (int i = 0; i < AbulletCount; ++i)
            {
                GameObject clone = Instantiate(bulletMedium, transform.position, Quaternion.identity);  // �߻�ü ����
                float angle = weightAngle + 6 + AintervalAngle * i;  // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);  // Cos(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);  // Sin(����), ���� ������ ���� ǥ���� ���� PI / 180�� ����
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));  // �߻�ü �̵� ���� ����
            }

            weightAngle += 10;  // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����

            count++;
            yield return new WaitForSeconds(attackRate);  // attackRate �ð� ��ŭ ���
        }


        StartCoroutine("MoveRightPattern");
    }

}
