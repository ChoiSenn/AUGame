using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMoving : MonoBehaviour
{
    Vector4 m_vecMouseDownPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_vecMouseDownPos = Input.mousePosition;

            // ���콺 Ŭ�� ��ġ�� ī�޶� ��ũ�� ��������Ʈ�� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

            // Raycast�Լ��� ���� �ε�ġ�� collider�� hit�� ���Ϲޱ�
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.name == "Coding1StageOn")
                {
                    SceneManager.LoadScene("Coding1Stage");
                }

                if (hit.collider.name == "Theory1StageOn")
                {
                    SceneManager.LoadScene("Theory1Stage");
                }

            }
        }
    }
}
