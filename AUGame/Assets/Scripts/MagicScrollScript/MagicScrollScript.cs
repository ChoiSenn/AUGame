using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollScript : MonoBehaviour
{
    // 다른 스크립트에서 쉽게 접근이 가능하도록 static
    public static bool GameIsPaused = false;  // 게임 정지하게 만들 때 사용
    public GameObject MagicScrollCanvas;

    public InputField CodeInput;  // 플레이어가 작성한 코드

    void Update()
    {
        
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void CompileButton()  // 컴파일 버튼
    {
        Debug.Log("컴파일!!");
        Debug.Log(CodeInput.text);  // InputField에 작성한 텍스트를 읽어옴
    }
}
