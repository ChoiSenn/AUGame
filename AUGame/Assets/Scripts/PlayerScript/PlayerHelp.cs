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
        if (Input.GetKeyDown(KeyCode.E) && (helpActive == false))  // e Ű�� ������
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

    void helpTextPrint()  // ĳ���� ��ġ�� ���� ���� ����ϴ� �Լ�
    {
        Collider2D[] collider2DsPlayer = Physics2D.OverlapBoxAll(posPlayer.position, boxSize, 0);  // ��� �浹 �˻�
        foreach (Collider2D collider in collider2DsPlayer)  // ���� ���� collider ����
        {
            Debug.Log("collider : " + collider.name);

            if(collider.name == "HelpAreaStart")  // ������ ���� HelpPanel �ؽ�Ʈ ����
            {
                image.sprite = spriteArr[1];
                helpText.text = "A, D Ű�� ������ �� �ְ�, \nSpaceBar�� ������ �� �־�. \n �ϴ� ���� �ѷ�����?";
            } else if(collider.name == "HelpAreaFire")
            {
                image.sprite = spriteArr[2];
                helpText.text = "��? ȶ���� �ֳ�?\n��Ŭ������ �������� �������� ����� �� ���� �� ����!";
                // ���� ó�� ������� �߰�
            }
            else if(collider.name == "HelpAreaEnemy")
            {
                image.sprite = spriteArr[2];
                helpText.text = "��, ���� �����ΰ�? ����ġ�ų� �����ؼ� �������߰ھ�...\n��Ƶ� ������ ����� ���� �� ����.";
            } else if(collider.name == "HelpAreaMagic")
            {
                image.sprite = spriteArr[1];
                helpText.text = "�������̴�! ������ ���� Q Ű�� ���� �������� ���ĺ���?";
                // ������ ó�� ������ ��/�ذ������� �߰�
            } else if(collider.name == "HelpAreaBar")
            {
                image.sprite = spriteArr[3];
                helpText.text = "���� �����ֳ�... �� ������ ������ ������ �� ���� �� ������?\n�� ������ �������� �̿��ϸ� �������� �� �� ���� ������...?";
            }
            else if (collider.name == "HelpAreaStair")
            {
                image.sprite = spriteArr[3];
                helpText.text = "���� �� �����ֳ�! ����� �� ���� �ִµ� ��� �ö���...?\n�ϴ� ���������� ����� �� �ִ� ���� ������ ���� Ȯ���غ���?";
            }
            // ������ �������� �� �̿��϶�� ��Ʈ �߰�
            //else
            //{
            //    image.sprite = spriteArr[1];
            //    helpText.text = "� ���� ���� �� ������ ���ڴ�~...";
            //}
        }
    }
}
