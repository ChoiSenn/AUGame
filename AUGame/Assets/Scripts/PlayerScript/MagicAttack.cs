using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public PlayerMoving player;

    public GameObject fire; // 불 이미지 받아오기
    public Transform pos; // 불이 발사될 위치 (Hierarchy 아래 player 아래 fireMagicpos 생성 후 위치 조정)
    public float cooltime;
    private float curtime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curtime <= 0)
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 시
            {
                if(player.magic == "fire")  // 현재 마법 여부에 따라 공격이 바뀜
                {
                    Instantiate(fire, pos.position, transform.rotation); // 받아온 불 이미지 발사
                }
                else
                {

                }
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

    }
}