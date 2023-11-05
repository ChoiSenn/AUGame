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

    public int userLevel = 0;
    public int userScore = 0;

    public int minValue = 0; // 랜덤 값의 최소값
    public int maxValue = 3; // 랜덤 값의 최대값

    List<string> TextArray = new List<string>();

    private string[] answer;

    private bool isKeyPressed = false; // 키 버튼 인풋
    private float keyPressedTime = 0.0f; // 시간 재기
    private float keyHoldDuration = 3.0f; // 3초간
    public Transform posPlayer;
    public Vector2 boxSize;

    void Start()
    {
        MagicCircle.SetActive(false);
        Quiz();
    }

    void Quiz()
    {
        TextArray.Add("(), {}, '', <>, (), 자료사전");

        string AnswerText = TextArray[0];
        answer = AnswerText.Split(',');

        RightAnswer = answer[4];
        QuizText.text = answer[5];

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

    List<int> GenerateRandomValues(int min, int max, int count)
    {
        // 중복 없는 랜덤 값을 저장할 리스트 생성
        List<int> randomValues = new List<int>();

        // 랜덤 값을 생성
        while (randomValues.Count < count)
        {
            int value = Random.Range(min, max); // 랜덤 값 생성

            // 중복되지 않은 값만 리스트에 추가
            if (!randomValues.Contains(value))
            {
                Debug.Log(value);
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
            }
            else if (collider.name == "A2area")
            {
                SelectAnswer = textMeshes[1].text;
            }
            else if (collider.name == "A3area")
            {
                SelectAnswer = textMeshes[2].text;
            }
            else if (collider.name == "A4area")
            {
                SelectAnswer = textMeshes[3].text;
            }

            if(SelectAnswer == RightAnswer)
            {
                Debug.Log("정답!");
                userScore += 10;
                SetScore();
                Quiz();
            }
        }
    }

    void SetScore()
    {
        ScoreText.text = "점수 : " + userScore.ToString();
        Debug.Log("Level : " + userLevel);
    }
}