using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public PlayerMoving player;

    public GameObject fire; // �� �̹��� �޾ƿ���
    public Transform pos; // ���� �߻�� ��ġ (Hierarchy �Ʒ� player �Ʒ� fireMagicpos ���� �� ��ġ ����)
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
            if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ�� ��
            {
                if(player.magic == "fire")  // ���� ���� ���ο� ���� ������ �ٲ�
                {
                    Instantiate(fire, pos.position, transform.rotation); // �޾ƿ� �� �̹��� �߻�
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