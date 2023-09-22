using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaPosition : MonoBehaviour
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
            GameObject.Find("AttackArea").transform.localPosition = new Vector3(-1.0f, 0, 0);
        }
        else // ������ ���� ���� ��
        {
            GameObject.Find("AttackArea").transform.localPosition = new Vector3(0.4f, 0, 0);
        }
    }
}
