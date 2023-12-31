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
    public GameObject Bar;  // 코드 입력 시 움직여질 바
    public Text ErrorText;  // 오류 띄울 우측 텍스트
    public Text HintText;  // 힌트 띄울 우측 텍스트

    public UIInventoryClick uiInven;
    public bool BlindOut = false;  // Bar를 변수 자리에 넣어 블라인드가 사라졌는지
    public GameObject BlindBtn;

    public PlayerMoving playerMoving;

    public int Bar_Y_Position = 0;
    Rigidbody2D rigid;

    public int failCount = 0;

    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip puzzelRight;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void CompileButton()  // 컴파일 버튼
    {
        // Debug.Log(CodeInput.text);  // InputField에 작성한 텍스트를 읽어옴
        string code = CodeInput.text;
        string errorMessage = "에러 메시지 없음";
        string hint = "힌트 없음";

        if(BlindOut == false)
        {
            errorMessage = "변수 할당이 잘못되었음. 움직이고자 하는 사물을 제대로 칸에 할당했는지 확인하세요!";
        }
        else if(code.Length <= 14 || code.Substring(0, 14) != "Bar_Y_Position")  // 코드 길이가 짧거나 앞에 Bar_Y_Position가 아니면 에러
        {
            errorMessage = "변경하고자 하는 변수의 값이 잘못되었음. 어떤 값을 변경해야 하는지 확인하세요!";

            failCount += 1;
        } else if(code.Substring(code.Length-1) != ";")  // 마지막에 ;로 안 끝나면
        {
            errorMessage = "구문 오류! 마지막에 ;를 붙였는지 확인하세요!";

            failCount += 1;
        }
        else
        {
            code = code.Substring(14);  // 변경하고자 하는 Bar_Y_Position 제거하고
            code = code.Substring(0, code.Length-1);  // ; 제거
            code = code.Replace(" ", string.Empty);  // 공백제거
            //string[] codeWords = code.Split();  // 공백으로 잘라서 해결할까 했는데 안 쓰는 경우도 있으니
            //for(int i = 0; i < codeWords.Length; i++)
            //{
            //    Debug.Log(codeWords[i]);
            //}
            if (code.Substring(0, 2) == "-=")  // -=일 경우에
            {
                code = code.Substring(2);
                Bar_Y_Position = int.Parse(code);
            } else if (code.Substring(0, 16) == "=Bar_Y_Position-")  // = 일 경우에
            {
                code = code.Substring(16);
                Bar_Y_Position = int.Parse(code);
            }
            else
            {
                errorMessage = "구문 오류! 코드를 다시 한 번 확인하세요!";

                failCount += 1;
            }
        }

        if (failCount == 1)  // n번 이상 구문 오류로 틀렸을 경우 추가 힌트 출력
        {
            hint = "hint 1. 발판을 낮추려면 어떤 코드를 작성해야 할지 생각해보자.";
        } else if (failCount == 2)
        {
            hint = "hint 1. 발판을 낮추려면 어떤 코드를 작성해야 할지 생각해보자.\nhint.2 발판의 Y축 값을 낮춰야 위치를 낮출 수 있다.";
        } else if (failCount >= 3)
        {
            hint = "hint 1. 발판을 낮추려면 어떤 코드를 작성해야 할지 생각해보자.\nhint 2. 발판의 Y축 값을 낮춰야 위치를 낮출 수 있다.\nhint 3. 변수의 값을 낮추기 위해서는 (변수) -= (값); 형식의 코드가 필요하다.";
        }

        if (Bar_Y_Position != 0)  // 0이 아닌 경우 바 이동
        {
            Time.timeScale = 1f;  // 시간 흐르게 만들고
            MagicScrollCanvas.SetActive(false);  // 마법스크롤 창 닫고
            playerMoving.MagicScrollCanvasFlag = false;

            Invoke("BarMovingFunc", 1);
            //BarMoving(Bar_Y_Position);
        }
        else
        {
            ErrorText.text = errorMessage;
            HintText.text = hint;
        }
    }

    void BarMovingFunc()
    {
        BarDown(Bar_Y_Position);
    }

    public GameObject explosion;

    void BarMoving(int bar_Y_Position)
    {
        StartCoroutine(BarMove(bar_Y_Position));
    }

    IEnumerator BarMove(float bar_Y_Position)
    {
        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(0 * 500, -1 * 200);

        yield return new WaitForSeconds(bar_Y_Position);

        rigid.velocity = new Vector2(1 * 0, 1 * 0);
        yield return new WaitForSeconds(1.5f);
    }

    void BarDown(int bar_Y_Position)
    {
        audioSource.PlayOneShot(puzzelRight);
        var explo = Instantiate(explosion, Bar.transform.position, Quaternion.identity);
        Destroy(explo, 0.5f);

        Vector3 destination = new Vector3(1251, 55 - (bar_Y_Position * 10), 0);
        Bar.transform.position = destination;
    }

    public void ResetButton()
    {
        Bar.transform.position = new Vector3(1251, 55, 0);
    }

    public void BlindButton()
    {
        Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (uiInven.nowMagic == "Bar Item")
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
