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
    public int nextMoveX;  //���� �ൿ��ǥ�� ������ ����
    public int nextMoveY;

    BigBatAI BigBatAI;

    void Start()
    {
        renderer = targetEnemy.GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody2D>();

        if (name.Equals("Slime"))  // �������� ���, ���� ����
        {
            hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
            SetEnemyStatus("Slime", 100, 10, 1.5f, 50, 1.5f, 650f);
            nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
        }
        else if (name.Equals("Bat"))  // ������ ���, ���� ����
        {
            SetEnemyStatus("Bat", 1, 10, 1.5f, 100, 1.5f, 400f);

            StartCoroutine("BatMoving");  // ���� ������ �ݺ�
        }
        else if (name.Equals("BigBat"))  // ū ���� ������ ���, ���� ����
        {
            hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
            SetEnemyStatus("BigBat", 1000, 10, 1.5f, 1000000, 1.5f, 400f);
            nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
            // �Ŵ� ���� �������� ���� ��ũ��Ʈ �ۼ�. ���߿� �Ϲ� ���㵵 ��ũ��Ʈ �и��Ұ�.
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

        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // �÷��̾��� ������ ����� �˻�
        foreach (Collider2D collider in collider2DsPlayer)
        {
            if (collider.tag == "PlayerAttack")  // ���� ���ݿ� ������
            {
                if (player.attacked)  // �÷��̾ ���� ���¸�
                {
                    if (name.Equals("Slime") || name.Equals("BigBat"))
                    {
                        nowHp -= 10;  // ������ �ް�
                        player.attacked = false;

                        if (nowHp <= 0) // �� ���
                        {
                            Die();
                        }
                        else
                        {
                            Attacked();  // ���� �޴� ���
                        }
                    } else if (name.Equals("Bat"))
                    {
                        Die();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)  // �ش� �±׿� ����� �� 
    {
        if (col.CompareTag("PlayerMagicAttack"))
        {  // ���� �߻�ü�� ������
            Destroy(col.gameObject);  // ���� �߻�ü ����

            if (name.Equals("Slime") || name.Equals("BigBat"))
            {
                nowHp -= 30;
                if (nowHp <= 0) // �� ���
                {
                    Die();
                }
                else
                {
                    Attacked();  // ���� �޴� ���
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

    void Attacked()  // ���ݴ��ϸ� ����Ÿ��� ��� �߰�
    {
        enemyAnimator.SetTrigger("Attacked");

        var explo = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explo, 0.5f);

        float dir = target.position.x - transform.position.x;
        if(dir < 0)  // ���� �÷��̾� ��ġ�� ���� �ڷ� �˹� ��
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
                enemyAnimator.SetTrigger("Die");            // die �ִϸ��̼� ����
                GetComponent<EnemySlimeAI>().enabled = false;    // ���� ��Ȱ��ȭ
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

            GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
            Destroy(GetComponent<Rigidbody2D>());       // �߷� ��Ȱ��ȭ

            Destroy(gameObject, 0.5f);                     // 0.5���� ����
            if (name.Equals("Slime"))
            {
                Destroy(hpBar.gameObject, 0.5f);               // 1���� ü�¹� ����
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

    IEnumerator BatMoving()  // ���� ������ �ϻ� ������
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);  // �ʸ��� ��� �����̱�

            nextMoveX = Random.Range(-1, 2);  // �¿� ������ ���� �̵����� ����
            nextMoveY = Random.Range(-1, 2);  // ���� ������ ���� �̵����� ����
            rigid.velocity = new Vector2(nextMoveX * 100, nextMoveY * 100);
        }
    }
}