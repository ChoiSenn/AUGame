using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaPosition : MonoBehaviour  // 좌우 이동 시에 스프라이트 rotation을 바꾸지 않고 스프라이트 이미지만 바꿔 이동할 때 사용하던 건데 PlayerMoving을 수정하여 좌우 이동 시에 rotation 자체를 바꿔주게 수정하여 필요없게 된 스크립트.
{
    public Animator animator;  // Animator 불러옴

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("MoveLeft")) // 왼쪽 보고 있을 때
        {
            //GameObject.Find("AttackArea").transform.localPosition = new Vector3(-1.0f, 0, 0);
            //GameObject.Find("bulletPos").transform.localPosition = new Vector3(-0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else // 오른쪽 보고 있을 때
        {
            //GameObject.Find("AttackArea").transform.localPosition = new Vector3(0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.localPosition = new Vector3(0.4f, 0, 0);
            //GameObject.Find("bulletPos").transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
