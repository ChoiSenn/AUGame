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
        if (name.Equals("Slime"))  // �������� ���, ���� ����
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

        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);  // �÷��̾��� ������ ����� �˻�
        foreach (Collider2D collider in collider2DsPlayer)
        {
            if (collider.tag == "PlayerAttack")  // ���� ���ݿ� ������
            {
                if (player.attacked)  // �÷��̾ ���� ���¸�
                {
                    //nowHp -= player.atkDmg;
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
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)  // �ش� �±׿� ����� �� 
    {
        if (col.CompareTag("PlayerMagicAttack"))
        {  // ���� �߻�ü�� ������
            Destroy(col.gameObject);  // ���� �߻�ü ����

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
    }

    public Transform target;

    void Attacked()  // ���ݴ��ϸ� ����Ÿ��� ��� �߰�
    {
        enemyAnimator.SetTrigger("Attacked");

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
        enemyAnimator.SetTrigger("Die");            // die �ִϸ��̼� ����
        Debug.Log("Slime Dying!!");
        GetComponent<EnemyAI>().enabled = false;    // ���� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
        Destroy(GetComponent<Rigidbody2D>());       // �߷� ��Ȱ��ȭ
        Destroy(gameObject, 1);                     // 1���� ����
        Destroy(hpBar.gameObject, 1);               // 1���� ü�¹� ����
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}