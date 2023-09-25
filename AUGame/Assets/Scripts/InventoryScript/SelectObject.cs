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

        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(1))
        {
            m_vecMouseDownPos = Input.mousePosition;

            // 마우스 클릭 위치를 카메라 스크린 월드포인트로 변경
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

            // Raycast함수를 통해 부딪치는 collider를 hit에 리턴받기
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                // 어떤 오브젝트인지 로그를 찍음
                //Debug.Log(hit.collider.name);

                // 오브젝트 별로 코드를 작성할 수 있음
                if (hit.collider.name == "Bar")
                {
                    //Debug.Log("!! Bar Hit !!");
                }

            }
        }

        


    }

}