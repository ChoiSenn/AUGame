using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTutorial1 : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public int clickCount = 0;

    // �̹��� ��ȯ �ڵ�
    public Image image;
    public Sprite[] spriteArr;

    void Start()
    {
        //image.sprite = spriteArr[0];
        talkPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // ��Ŭ�� ������
        {
            tutorialText1(clickCount);
            clickCount++;
        }
    }

    void tutorialText1(int clickCount)
    {
        switch (clickCount)
        {
            case 0:
                talkPanel.SetActive(true);

                image.sprite = spriteArr[1];
                talkText.text = "ù ��° �������� ����? \n ��ٸ��� �� �ɻ� �� �°ŵ�.";
                break;

            case 1:
                image.sprite = spriteArr[2];
                talkText.text = "������ڰ�~";
                break;

            case 2:
                image.sprite = spriteArr[3];
                talkPanel.SetActive(false);
                break;

            default:
                Debug.Log("Default : " + clickCount);  // ����׿�
                break;
        }
    }
}