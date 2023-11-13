using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollT1 : MonoBehaviour
{
    // 다른 스크립트에서 쉽게 접근이 가능하도록 static
    public static bool GameIsPaused = false;  // 게임 정지하게 만들 때 사용
    public GameObject MagicScrollCanvas;

    public InputField CodeInput;  // 플레이어가 작성한 코드
    public Text ErrorText;  // 오류 띄울 우측 텍스트
    public Text HintText;  // 힌트 띄울 우측 텍스트

    public GameObject Block;  // 정답 시 사라질 블록 (TODO : 문으로 변경)
    public GameObject Block2;
    public GameObject Block3;
    public GameObject Block4;
    public GameObject Block5;

    public PlayerMoving playerMoving;
    public int failCount = 0;

    public AudioSource audioSource;
    public AudioClip doorOpen;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
    }

    public void CompileButton()  // 컴파일 버튼
    {
        // Debug.Log(CodeInput.text);  // InputField에 작성한 텍스트를 읽어옴
        string code = CodeInput.text;
        string errorMessage = "에러 메시지 없음";
        string hint = "힌트 없음";

        if (code == "open the door")
        {
            errorMessage = " 정답입니다 !!";
            ErrorText.text = errorMessage;

            OpenDoor();
        }
        else
        {
             errorMessage = " 틀렸습니다 !!";
             failCount += 1;

            if (failCount == 1)  // n번 이상 구문 오류로 틀렸을 경우 추가 힌트 출력
            {
                hint = "hint 1. a[3:6]은 문자열 a의 3번째 자리부터 6번째의 자리까지 자른다는 뜻이다.";
            }
            else if (failCount == 2)
            {
                hint = "hint 1. a[3:6]은 문자열 a의 3번째 자리부터 6번째의 자리까지 자른다는 뜻이다.\nhint 2. %s는 % 뒤의 추가된 문자를 문자열로 해당 자리에 삽입한다는 뜻이다.";
            }
            else if (failCount >= 3)
            {
                hint = "hint 1. a[3:6]은 문자열 a의 3번째 자리부터 6번째의 자리까지 자른다는 뜻이다.\nhint 2. %s는 % 뒤의 추가된 문자를 문자열로 해당 자리에 삽입한다는 뜻이다.\nhint 3. 파이썬에서는 '+'로 문자열을 이어붙일 수 있다.";
            }

            ErrorText.text = errorMessage;
            HintText.text = hint;
        }
    }

    public GameObject explosion;

    void OpenDoor()
    {
        Time.timeScale = 1f;  // 시간 흐르게 만들고
        MagicScrollCanvas.SetActive(false);  // 마법스크롤 창 닫고
        playerMoving.MagicScrollCanvasFlag = false;
        audioSource.PlayOneShot(doorOpen);

        var explo = Instantiate(explosion, Block.transform.position, Quaternion.identity);
        var explo2 = Instantiate(explosion, Block2.transform.position, Quaternion.identity);
        var explo3 = Instantiate(explosion, Block3.transform.position, Quaternion.identity);
        var explo4 = Instantiate(explosion, Block4.transform.position, Quaternion.identity);
        var explo5 = Instantiate(explosion, Block5.transform.position, Quaternion.identity);
        Destroy(Block, 0.1f);
        Destroy(Block2, 0.1f);
        Destroy(Block3, 0.1f);
        Destroy(Block4, 0.1f);
        Destroy(Block5, 0.1f);
        Destroy(explo, 0.5f);
        Destroy(explo2, 0.5f);
        Destroy(explo3, 0.5f);
        Destroy(explo4, 0.5f);
        Destroy(explo5, 0.5f);
    }

}
