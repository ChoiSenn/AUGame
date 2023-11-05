using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theory1EKey : MonoBehaviour
{
    private bool isKeyPressed = false;
    private float keyPressedTime = 0.0f; // 시간 재기
    private float keyHoldDuration = 3.0f;

    public Transform posPlayer;
    public Vector2 boxSize;

    public T1StageEvent T1StageEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isKeyPressed = true;
            keyPressedTime = Time.time;
        }

        if (Input.GetKey(KeyCode.E) && isKeyPressed)
        {
            float elapsed = Time.time - keyPressedTime;

            //Debug.Log("E key was held for " + elapsed + " seconds");

            // 누른 시간이 3초가 되면 디버그 메시지 출력
            if (elapsed >= keyHoldDuration)
            {
                isKeyPressed = false;
                Debug.Log("E key was held for " + keyHoldDuration + " seconds");
                T1StageEvent.AnswerPrint();
            }
        }

        // "E" 키가 떼어졌을 때
        if (Input.GetKeyUp(KeyCode.E) && isKeyPressed)
        {
            isKeyPressed = false;
        }

    }
}