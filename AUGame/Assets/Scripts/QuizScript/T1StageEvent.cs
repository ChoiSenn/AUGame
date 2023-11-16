using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T1StageEvent : MonoBehaviour
{
    public static T1StageEvent instance;

    public GameObject MagicCircle;

    public Text QuizText;
    public string RightAnswer;
    public TextMesh[] textMeshes; // TextMesh 컴포넌트 배열
    public Text ScoreText;
    public Text LevelText;

    public int userLevel = 0;
    public int userScore = 0;

    public int minValue = 2; // 랜덤 값의 최소값
    public int maxValue = 6; // 랜덤 값의 최대값

    List<string> TextArray = new List<string>();

    private string[] answer;

    private bool isKeyPressed = false; // 키 버튼 인풋
    private float keyPressedTime = 0.0f; // 시간 재기
    private float keyHoldDuration = 3.0f; // 3초간
    public Transform posPlayer;
    public Vector2 boxSize;

    public AudioSource audioSource; 
    public AudioClip RightSound; 
    public AudioClip WrongSound;

    public GameObject smallBat;
    public Transform target;

    void Start()
    {
        MagicCircle.SetActive(false);
        
        List<Dictionary<string, object>> quiz_data = CSVReader.Read("Thoery1");

        for (int i = 0; i < quiz_data.Count; i++)
        {
            TextArray.Add(quiz_data[i]["level"].ToString() + "," + quiz_data[i]["Q"].ToString() + "," + quiz_data[i]["A"].ToString() + "," + quiz_data[i]["A1"].ToString() + "," + quiz_data[i]["A2"].ToString() + "," + quiz_data[i]["A3"].ToString());
        }

        Quiz();
    }

    void Quiz()
    {
        string AnswerText = CSVRandomQuiz();

        answer = AnswerText.Split(',');

        RightAnswer = answer[2];
        QuizText.text = answer[1];

        Debug.Log("정답 : " + RightAnswer);

        // 중복 없는 랜덤 값을 생성
        List<int> randomValues = GenerateRandomValues(minValue, maxValue, textMeshes.Length);

        // 생성한 랜덤 값을 j에 넣고, 랜덤 값 위치를 answer로 받아와 TextMesh 컴포넌트의 text 속성에 할당
        for (int i = 0; i < textMeshes.Length; i++)
        {
            int j = randomValues[i];
            textMeshes[i].text = answer[j];
        }
    }

    int loopNum = 0;

    string CSVRandomQuiz()  // CSV 읽어온 문제 리스트에서 문제 랜덤 뽑아오기
    {
        while (true)
        {
            if (loopNum++ > 100)
                throw new System.Exception("무한루프");  // 무한루프 방지

            int randomValue = Random.Range(0, 36);  // CSV 문제 랜덤 추출을 위한 행의 랜덤값

            if (int.Parse(TextArray[randomValue][0].ToString()) == userLevel)  // user의 현재 수준과 맞는 문제만 출력
            {
                return TextArray[randomValue];
            }
            //Debug.Log(randomValue + " : " + TextArray[randomValue]);
        }
    }

    List<int> GenerateRandomValues(int min, int max, int count)
    {
        // 중복 없는 랜덤 값을 저장할 리스트 생성
        List<int> randomValues = new List<int>();

        // 랜덤 값을 생성
        while (randomValues.Count < count)
        {
            int value = Random.Range(2, 6); // 랜덤 값 생성

            // 중복되지 않은 값만 리스트에 추가
            if (!randomValues.Contains(value))
            {
                randomValues.Add(value);
            }
        }

        return randomValues;
    }

    private void Update()
    {
        // e가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.E))
        {
            isKeyPressed = true;
            keyPressedTime = Time.time;
        }

        if (Input.GetKey(KeyCode.E) && isKeyPressed)
        {
            float elapsed = Time.time - keyPressedTime;

            // 누른 시간이 3초가 되면 디버그 메시지 출력
            if (elapsed >= keyHoldDuration)
            {
                isKeyPressed = false;
                //Debug.Log("E key was held for " + keyHoldDuration + " seconds");
                AnswerPrint();
            }
        }

        // "E" 키가 떼어졌을 때
        if (Input.GetKeyUp(KeyCode.E) && isKeyPressed)
        {
            isKeyPressed = false;
        }
    }

    public void AnswerPrint()
    {
        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // 닿는 충돌 검사
        foreach (Collider2D collider in collider2DsPlayer)  // 현재 닿은 collider 감지
        {
            string SelectAnswer = "";

            if (collider.name == "A1area")
            {
                SelectAnswer = textMeshes[0].text;
                isCollect(SelectAnswer);
            }
            else if (collider.name == "A2area")
            {
                SelectAnswer = textMeshes[1].text;
                isCollect(SelectAnswer);
            }
            else if (collider.name == "A3area")
            {
                SelectAnswer = textMeshes[2].text;
                isCollect(SelectAnswer);
            }
            else if (collider.name == "A4area")
            {
                SelectAnswer = textMeshes[3].text;
                isCollect(SelectAnswer);
            }
        }
    }

    void isCollect(string SelectAnswer)
    {
        if (SelectAnswer == RightAnswer)
        {
            Debug.Log("정답!");
            userScore += 10;
            if (userLevel < 3)
            {
                userLevel += 1;
            }
            audioSource.PlayOneShot(RightSound);
            SetScore();
            Quiz();
        }
        else if (SelectAnswer != RightAnswer)
        {
            userScore -= 5;
            Debug.Log("오답!");
            if (userLevel > 0)
            {
                userLevel -= 1;
            }
            audioSource.PlayOneShot(WrongSound);
            SetScore();
            //enemySpawn();
        }
    }

    void enemySpawn()  // 문제 틀렸을 시, 작은 박쥐 몬스터 스폰
    {
        if (target.position.x >= 0)   // 플레이어 위치의 x좌표가 양수이면 왼쪽에 생성, 음수이면 오른쪽에 생성
        {
            Instantiate(smallBat, new Vector3(-500, -30, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(smallBat, new Vector3(500, -30, 0), Quaternion.identity);
        }
    }

    void SetScore()
    {
        ScoreText.text = "점수 : " + userScore.ToString();
        LevelText.text = "레벨 : " + userLevel.ToString();
        Debug.Log("Level : " + userLevel);

        if (userScore >= 50)
        {
            CreateMagicCircle();
        }
    }

    void CreateMagicCircle()  // 50점 이상이면 마법진 생성
    {
        MagicCircle.SetActive(true);
    }
}