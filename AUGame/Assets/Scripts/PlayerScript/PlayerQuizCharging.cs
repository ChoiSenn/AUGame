using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuizCharging : MonoBehaviour
{
    public GameObject ChargingBar;
    public GameObject canvas;

    public float height = 44.0f;

    [SerializeField]
    private Slider Bar;
    public GameObject obj_Bar;

    public Transform posPlayer;
    public Vector2 boxSize;

    float keyPressedTime = 0.0f;
    float elapsed = 0.0f;

    void Start()
    {
        obj_Bar = GameObject.Find("QuizCanvas/Slider");
        Bar.value = 0.0f;
    }

    void Update()
    {
        Vector3 _BarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        obj_Bar.transform.position = _BarPos;

        Bar.value = elapsed;

        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // 닿는 충돌 검사
        foreach (Collider2D collider in collider2DsPlayer)  // 현재 닿은 collider 감지
        {
            if (collider.name == "A1area" || collider.name == "A2area" || collider.name == "A3area" || collider.name == "A4area")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    keyPressedTime = Time.time;
                }

                if (Input.GetKey(KeyCode.E))
                {
                    elapsed = Time.time - keyPressedTime;
                }

                if (Input.GetKeyUp(KeyCode.E))
                {
                    elapsed = 0.0f;
                }
            }
        }
    }
}
