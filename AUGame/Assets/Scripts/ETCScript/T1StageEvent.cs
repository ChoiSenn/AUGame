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
    public TextMesh[] textMeshes; // TextMesh ������Ʈ �迭
    public Text ScoreText;

    public int userLevel = 0;
    public int userScore = 0;

    public int minValue = 0; // ���� ���� �ּҰ�
    public int maxValue = 3; // ���� ���� �ִ밪

    List<string> TextArray = new List<string>();

    private string[] answer;

    private bool isKeyPressed = false; // Ű ��ư ��ǲ
    private float keyPressedTime = 0.0f; // �ð� ���
    private float keyHoldDuration = 3.0f; // 3�ʰ�
    public Transform posPlayer;
    public Vector2 boxSize;

    void Start()
    {
        MagicCircle.SetActive(false);
        Quiz();
    }

    void Quiz()
    {
        TextArray.Add("(), {}, '', <>, (), �ڷ����");

        string AnswerText = TextArray[0];
        answer = AnswerText.Split(',');

        RightAnswer = answer[4];
        QuizText.text = answer[5];

        Debug.Log("���� : " + RightAnswer);

        // �ߺ� ���� ���� ���� ����
        List<int> randomValues = GenerateRandomValues(minValue, maxValue, textMeshes.Length);

        // ������ ���� ���� j�� �ְ�, ���� �� ��ġ�� answer�� �޾ƿ� TextMesh ������Ʈ�� text �Ӽ��� �Ҵ�
        for (int i = 0; i < textMeshes.Length; i++)
        {
            int j = randomValues[i];
            textMeshes[i].text = answer[j];
        }
    }

    List<int> GenerateRandomValues(int min, int max, int count)
    {
        // �ߺ� ���� ���� ���� ������ ����Ʈ ����
        List<int> randomValues = new List<int>();

        // ���� ���� ����
        while (randomValues.Count < count)
        {
            int value = Random.Range(min, max); // ���� �� ����

            // �ߺ����� ���� ���� ����Ʈ�� �߰�
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
        // e�� ������ ��
        if (Input.GetKeyDown(KeyCode.E))
        {
            isKeyPressed = true;
            keyPressedTime = Time.time;
        }

        if (Input.GetKey(KeyCode.E) && isKeyPressed)
        {
            float elapsed = Time.time - keyPressedTime;

            // ���� �ð��� 3�ʰ� �Ǹ� ����� �޽��� ���
            if (elapsed >= keyHoldDuration)
            {
                isKeyPressed = false;
                //Debug.Log("E key was held for " + keyHoldDuration + " seconds");
                AnswerPrint();
            }
        }

        // "E" Ű�� �������� ��
        if (Input.GetKeyUp(KeyCode.E) && isKeyPressed)
        {
            isKeyPressed = false;
        }
    }

    public void AnswerPrint()
    {
        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // ��� �浹 �˻�
        foreach (Collider2D collider in collider2DsPlayer)  // ���� ���� collider ����
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
                Debug.Log("����!");
                userScore += 10;
                SetScore();
                Quiz();
            }
        }
    }

    void SetScore()
    {
        ScoreText.text = "���� : " + userScore.ToString();
        Debug.Log("Level : " + userLevel);
    }
}