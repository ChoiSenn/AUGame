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

            // 마우스 클릭 위치를 카메라 스크린 월드포인트로 변경
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

            // Raycast함수를 통해 부딪치는 collider를 hit에 리턴받기
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
