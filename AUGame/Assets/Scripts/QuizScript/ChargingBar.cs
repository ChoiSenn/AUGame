using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingBar : MonoBehaviour
{
    [SerializeField]
    private Slider Bar;

    private float maxCharge = 3.0f;

    public Transform posPlayer;
    public Vector2 boxSize;

    float keyPressedTime = 0.0f;
    float elapsed = 0.0f;

    void Start()
    {
        //Bar.gameObject.SetActive(false);
        Bar.value = 0.0f;
    }

    void Update()
    {
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
            }
        }
    }
}
