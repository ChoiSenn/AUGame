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
    public Sprite[] spriteArr;

    public bool helpActive;

    public Transform posPlayer;
    public Vector2 boxSize;

    void Start()
    {
        helpPanel.SetActive(false);
        helpActive = false;
    }

    void Update()
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

    void helpTextPrint()  // 캐릭터 위치에 따라 도움말 출력하는 함수
    {
        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // 닿는 충돌 검사
        foreach (Collider2D collider in collider2DsPlayer)  // 현재 닿은 collider 감지
        {
            Debug.Log("collider : " + collider.name);

            if(collider.name == "HelpAreaStart")  // 영역에 따라 HelpPanel 텍스트 수정
            {
                image.sprite = spriteArr[1];
                helpText.text = "A, D 키로 움직일 수 있고, \nSpaceBar로 점프할 수 있어. \n 일단 조금 둘러볼까?";
            } else if(collider.name == "HelpAreaFire")
            {
                image.sprite = spriteArr[2];
                helpText.text = "응? 횃불이 있네?\n우클릭으로 마법으로 변형시켜 사용할 수 있을 것 같아!";
                // 마법 처음 담았을때 추가
            }
            else if(collider.name == "HelpAreaEnemy")
            {
                image.sprite = spriteArr[2];
                helpText.text = "헉, 저건 몬스터인가? 물리치거나 조심해서 지나가야겠어...\n담아둔 마법을 써봐도 좋을 것 같네.";
            } else if(collider.name == "HelpAreaMagic")
            {
                image.sprite = spriteArr[1];
                helpText.text = "마법진이다! 가까이 가서 Q 키를 눌러 마법서를 펼쳐볼까?";
                // 마법서 처음 펼쳤을 때/해결했을때 추가
            } else if(collider.name == "HelpAreaBar")
            {
                image.sprite = spriteArr[3];
                helpText.text = "길이 끊겨있네... 저 발판을 내리면 지나갈 수 있을 것 같은데?\n저 발판을 마법으로 이용하면 내려오게 할 수 있지 않을까...?";
            }
            else if (collider.name == "HelpAreaStair")
            {
                image.sprite = spriteArr[3];
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
