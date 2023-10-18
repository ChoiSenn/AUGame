using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHelp : MonoBehaviour
{
    public GameObject helpPanel;
    public TextMeshProUGUI helpText;

    public Image image;
    public Image image2;
    public Sprite[] spriteArr;

    public bool helpActive;

    public Transform posPlayer;
    public Vector2 boxSize;

    public bool startScript = false;
    public int clickCount = 0;

    void Start()
    {
        helpPanel.SetActive(true);
        helpActive = true;
        Time.timeScale = 0f;

        image.sprite = spriteArr[0];
        image2.sprite = spriteArr[0];
    }

    void Update()
    {
        if (startScript == false)  // 시작 스크립트 안 봤으면 시작 스크립트 출력
        {
            if (Input.GetMouseButtonDown(0))  // 좌클릭 눌리면
            {
                startEventTextPrint(clickCount);
                clickCount++;
            }
        }
        else  // 시작 스크립트 보고 난 후에만 입력 받기
        {
            if (Input.GetKeyDown(KeyCode.E) && (helpActive == false))  // e 키가 눌리면
            {
                helpPanel.SetActive(true);
                helpActive = true;
                Time.timeScale = 0f;

                helpTextPrint();
            }
            else if (Input.GetKeyDown(KeyCode.E) && (helpActive == true))
            {
                helpPanel.SetActive(false);
                helpActive = false;
                Time.timeScale = 1f;
            }
        }
    }

    void startEventTextPrint(int clickCount)  // 첫 시작 시 출력되는 대화
    {
        switch (clickCount)
        {
            case 0:
                image.sprite = spriteArr[1];
                image2.sprite = spriteArr[0];  // 투명하게 만들어 안 보이게 하기
                helpText.text = "생각보다 안이 어둡네요... 조금 무서운 것 같아요.";
                break;

            case 1:
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "헉, 저기 보이는 건 몬스터인가요?";
                break;

            case 2:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[6];
                helpText.text = "그래, 몬스터가 몇 마리 돌아다니고 있는데 마법을 이용하면 쉽게 잡을 수 있을 거라고 믿는다.";
                break;

            case 3:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[7];
                helpText.text = "그리고 도중에 길이 끊겨있다면 주변 사물과 마법 주문을 이용해서 지나갈 수 있도록 노력해보렴.";
                break;

            case 4:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[6];
                helpText.text = "나는 이쪽에서 기다리고 있을테니, 한 번 혼자 해결해보거라.";
                break;

            case 5:
                image.sprite = spriteArr[4];
                image2.sprite = spriteArr[0];
                helpText.text = "네, 스승님!";
                break;

            case 6:
                helpPanel.SetActive(false);
                helpActive = false;
                Time.timeScale = 1f;
                startScript = true;
                break;

            default:
                Debug.Log("Start Script : " + clickCount);  // 디버그용
                break;
        }
        

    }

    void helpTextPrint()  // 캐릭터 위치에 따라 도움말 출력하는 함수
    {
        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // 닿는 충돌 검사
        foreach (Collider2D collider in collider2DsPlayer)  // 현재 닿은 collider 감지
        {
            Debug.Log("collider : " + collider.name);

            if(collider.name == "HelpAreaStart")  // 영역에 따라 HelpPanel 텍스트 수정
            {
                image.sprite = spriteArr[1];
                image2.sprite = spriteArr[0];
                helpText.text = "A, D 키로 움직일 수 있고, \nSpaceBar로 점프할 수 있어. \n 일단 조금 둘러볼까?";
            } else if(collider.name == "HelpAreaFire")
            {
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "응? 횃불이 있네?\n우클릭으로 마법으로 변형시켜 사용할 수 있을 것 같아!";
                // 마법 처음 담았을때 추가
            }
            else if(collider.name == "HelpAreaEnemy")
            {
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "헉, 저건 몬스터인가? 물리치거나 조심해서 지나가야겠어...\n담아둔 마법을 써봐도 좋을 것 같네.";
            } else if(collider.name == "HelpAreaMagic")
            {
                image.sprite = spriteArr[1];
                image2.sprite = spriteArr[0];
                helpText.text = "마법진이다! 가까이 가서 Q 키를 눌러 마법서를 펼쳐볼까?";
                // 마법서 처음 펼쳤을 때/해결했을때 추가
            } else if(collider.name == "HelpAreaBar")
            {
                image.sprite = spriteArr[3];
                image2.sprite = spriteArr[0];
                helpText.text = "길이 끊겨있네... 저 발판을 내리면 지나갈 수 있을 것 같은데?\n저 발판을 마법으로 이용하면 내려오게 할 수 있지 않을까...?";
            }
            else if (collider.name == "HelpAreaStair")
            {
                image.sprite = spriteArr[3];
                image2.sprite = spriteArr[0];
                helpText.text = "길이 또 끊겨있네! 계단은 저 위에 있는데 어떻게 올라가지...?\n일단 마법진에서 사용할 수 있는 마법 공식을 먼저 확인해볼까?";
            }
            // 마법서 펼쳤을때 블럭 이용하라는 힌트 추가
            //else
            //{
            //    image.sprite = spriteArr[1];
            //    helpText.text = "어서 정상에 오를 수 있으면 좋겠다~...";
            //}
        }
    }
}
