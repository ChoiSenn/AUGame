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

    // 이미지 변환 코드
    public Image image;
    public Sprite[] spriteArr;

    void Start()
    {
        //image.sprite = spriteArr[0];
        talkPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 좌클릭 눌리면
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
                talkText.text = "첫 번째 관문으로 들어갈까? \n 기다리는 건 심상에 안 맞거든.";
                break;

            case 1:
                image.sprite = spriteArr[2];
                talkText.text = "출발하자고~";
                break;

            case 2:
                image.sprite = spriteArr[3];
                talkPanel.SetActive(false);
                break;

            default:
                Debug.Log("Default : " + clickCount);  // 디버그용
                break;
        }
    }
}