using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollScript2 : MonoBehaviour
{
    // 다른 스크립트에서 쉽게 접근이 가능하도록 static
    public static bool GameIsPaused = false;  // 게임 정지하게 만들 때 사용
    public GameObject MagicScrollCanvas2;

    public InputField CodeInput1;  // 플레이어가 작성한 코드
    public InputField CodeInput2;  // 플레이어가 작성한 코드2
    public InputField CodeInput3;  // 플레이어가 작성한 코드3
    public GameObject Block;  // 코드 입력 시 생성될 블럭
    public Text ErrorText;  // 오류 띄울 우측 텍스트
    public Text HintText;  // 힌트 띄울 우측 텍스트

    public int forCount;  // for문 돌릴 개수

    public UIInventoryClick uiInven;
    public bool BlindOut = false;  // Bar를 변수 자리에 넣어 블라인드가 사라졌는지
    public GameObject BlindBtn;

    public PlayerMoving playerMoving;

    public int failCount = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Resume()
    {
        MagicScrollCanvas2.SetActive(false);
    }

    public void Pause()
    {
        MagicScrollCanvas2.SetActive(true);
    }

    public void CompileButton()  // 컴파일 버튼
    {
        // Debug.Log(CodeInput.text);  // InputField에 작성한 텍스트를 읽어옴
        forCount = int.Parse(CodeInput1.text);
        string code1 = CodeInput2.text.Replace(" ", string.Empty);
        string code2 = CodeInput3.text.Replace(" ", string.Empty);
        string errorMessage = "에러 메시지 없음";
        string hint = "힌트 없음";

        if (code1 != "j>=i")  // 두 번째 빈칸의 코드가 틀렸을 경우
        {
            errorMessage = "for문의 첫 번째 빈 칸의 코드가 틀렸음. 계단 형태를 만들기 위해서는 어떤 모양으로 공백이 들어가야 할지 생각해보세요!";
            ErrorText.text = errorMessage;
            HintText.text = hint;
            failCount += 1;
        }
        else if (code2 != "k<=i")  // 세 번째 빈칸의 코드가 틀렸을 경우
        {
            errorMessage = "for문의 두 번째 빈 칸의 코드가 틀렸음. 계단 형태를 만들기 위해서는 어떤 모양으로 블럭이 들어가야 할지 생각해보세요!";
            ErrorText.text = errorMessage;
            HintText.text = hint;
            failCount += 1;
        } else  // 둘 다 맞았으면
        {
            Debug.Log("계단 형태 출력 : " + forCount);

            Time.timeScale = 1f;  // 시간 흐르게 만들고
            MagicScrollCanvas2.SetActive(false);  // 마법스크롤 창 닫고
            playerMoving.MagicScrollCanvasFlag = false;

            Invoke("BlockStair", 1);
        }

        if (failCount == 1)  // n번 이상 구문 오류로 틀렸을 경우 추가 힌트 출력
        {
            hint = "hint 1. 벽돌을 계단 형식으로 쌓으려면 어떤 코드를 작성해야 할지 생각해보자.";
            HintText.text = hint;
        }
        else if (failCount >= 2)
        {
            hint = "hint 1. 벽돌을 계단 형식으로 쌓으려면 어떤 코드를 작성해야 할지 생각해보자.\nhint.2 첫 번째 칸은 공백을 출력하는 반복문이고, 두 번째 칸은 벽돌을 출력하는 반복문임에 주목하자.";
            HintText.text = hint;
        }
        //else if (failCount >= 3)
        //{
        //    hint = "hint 1. 벽돌을 계단 형식으로 쌓으려면 어떤 코드를 작성해야 할지 생각해보자.\nhint 2. 첫 번째 칸은 공백을 출력하는 반복문이고, 두 번째 칸은 벽돌을 출력하는 반복문임에 주목하자.\nhint 3. 첫 번째 칸은 .";
        //    HintText.text = hint;
        //}
        else
        {
            HintText.text = hint;
        }
    }

    void BlockStair()
    {
        BlockStairStart(forCount);  // 블럭 쌓기
    }

    public GameObject explosion;

    void BlockStairStart(int Count)
    {
        if (Count >= 0)
        {
            int blockX = 1866;

            for (int i = 0; i < Count; i++)
            {
                Instantiate(Block, new Vector3(blockX, 300, 0), Quaternion.identity);
                blockX -= 19;
            }

            forCount = Count - 1;
            Invoke("BlockStair", 0.2f);  // 재귀해서 계단 모양으로 블럭 쌓기
        }
    }

    public void ResetButton()
    {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("CreateBlock");

        Debug.Log(temp.Length + "개 찾음");

        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
    }

    public void BlindButton()
    {
        Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (uiInven.nowMagic == "Block Item")
        {
            Destroy(BlindBtn);
            BlindOut = true;
        }
        else
        {
            ErrorText.text = "이동시켜야 할 물체가 틀렸습니다!";
        }
    }
}
