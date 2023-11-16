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
    public Text LevelText;

    public int userLevel = 0;
    public int userScore = 0;

    public int minValue = 2; // ���� ���� �ּҰ�
    public int maxValue = 6; // ���� ���� �ִ밪

    List<string> TextArray = new List<string>();

    private string[] answer;

    private bool isKeyPressed = false; // Ű ��ư ��ǲ
    private float keyPressedTime = 0.0f; // �ð� ���
    private float keyHoldDuration = 3.0f; // 3�ʰ�
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

    int loopNum = 0;

    string CSVRandomQuiz()  // CSV �о�� ���� ����Ʈ���� ���� ���� �̾ƿ���
    {
        while (true)
        {
            if (loopNum++ > 100)
                throw new System.Exception("���ѷ���");  // ���ѷ��� ����

            int randomValue = Random.Range(0, 36);  // CSV ���� ���� ������ ���� ���� ������

            if (int.Parse(TextArray[randomValue][0].ToString()) == userLevel)  // user�� ���� ���ذ� �´� ������ ���
            {
                return TextArray[randomValue];
            }
            //Debug.Log(randomValue + " : " + TextArray[randomValue]);
        }
    }

    List<int> GenerateRandomValues(int min, int max, int count)
    {
        // �ߺ� ���� ���� ���� ������ ����Ʈ ����
        List<int> randomValues = new List<int>();

        // ���� ���� ����
        while (randomValues.Count < count)
        {
            int value = Random.Range(2, 6); // ���� �� ����

            // �ߺ����� ���� ���� ����Ʈ�� �߰�
            if (!randomValues.Contains(value))
            {
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
            Debug.Log("����!");
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
            Debug.Log("����!");
            if (userLevel > 0)
            {
                userLevel -= 1;
            }
            audioSource.PlayOneShot(WrongSound);
            SetScore();
            //enemySpawn();
        }
    }

    void enemySpawn()  // ���� Ʋ���� ��, ���� ���� ���� ����
    {
        if (target.position.x >= 0)   // �÷��̾� ��ġ�� x��ǥ�� ����̸� ���ʿ� ����, �����̸� �����ʿ� ����
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
        ScoreText.text = "���� : " + userScore.ToString();
        LevelText.text = "���� : " + userLevel.ToString();
        Debug.Log("Level : " + userLevel);

        if (userScore >= 50)
        {
            CreateMagicCircle();
        }
    }

    void CreateMagicCircle()  // 50�� �̻��̸� ������ ����
    {
        MagicCircle.SetActive(true);
    }
}