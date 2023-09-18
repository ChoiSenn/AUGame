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
            Debug.Log("left");
            GameObject.Find("AttackArea").transform.localPosition = new Vector3(-0.4f, 0, 0);
        }
        else // ������ ���� ���� ��
        {
            Debug.Log("right");
            GameObject.Find("AttackArea").transform.localPosition = new Vector3(0.4f, 0, 0);
        }
    }
}
