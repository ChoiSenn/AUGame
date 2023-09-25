using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    Vector4 m_vecMouseDownPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // ���콺 Ŭ�� ��
        if (Input.GetMouseButtonDown(1))
        {
            m_vecMouseDownPos = Input.mousePosition;

            // ���콺 Ŭ�� ��ġ�� ī�޶� ��ũ�� ��������Ʈ�� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

            // Raycast�Լ��� ���� �ε�ġ�� collider�� hit�� ���Ϲޱ�
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                // � ������Ʈ���� �α׸� ����
                //Debug.Log(hit.collider.name);

                // ������Ʈ ���� �ڵ带 �ۼ��� �� ����
                if (hit.collider.name == "Bar")
                {
                    //Debug.Log("!! Bar Hit !!");
                }

            }
        }

        


    }

}