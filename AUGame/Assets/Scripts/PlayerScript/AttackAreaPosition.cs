using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaPosition : MonoBehaviour  // �¿� �̵� �ÿ� ��������Ʈ rotation�� �ٲ��� �ʰ� ��������Ʈ �̹����� �ٲ� �̵��� �� ����ϴ� �ǵ� PlayerMoving�� �����Ͽ� �¿� �̵� �ÿ� rotation ��ü�� �ٲ��ְ� �����Ͽ� �ʿ���� �� ��ũ��Ʈ.
{
    public Animator animator;  // Animator �ҷ���

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("MoveLeft")) // ���� ���� ���� ��
        {
            //GameObject.Find("AttackArea").transform.localPosition = new Vector3(-1.0f, 0, 0);
            //GameObject.Find("bulletPos").transform.localPosition = new Vector3(-0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else // ������ ���� ���� ��
        {
            //GameObject.Find("AttackArea").transform.localPosition = new Vector3(0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.localPosition = new Vector3(0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
