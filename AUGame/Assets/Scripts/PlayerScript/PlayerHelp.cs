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
        if (startScript == false)  // ���� ��ũ��Ʈ �� ������ ���� ��ũ��Ʈ ���
        {
            if (Input.GetMouseButtonDown(0))  // ��Ŭ�� ������
            {
                startEventTextPrint(clickCount);
                clickCount++;
            }
        }
        else  // ���� ��ũ��Ʈ ���� �� �Ŀ��� �Է� �ޱ�
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
    }

    void startEventTextPrint(int clickCount)  // ù ���� �� ��µǴ� ��ȭ
    {
        switch (clickCount)
        {
            case 0:
                image.sprite = spriteArr[1];
                image2.sprite = spriteArr[0];  // �����ϰ� ����� �� ���̰� �ϱ�
                helpText.text = "�������� ���� ��ӳ׿�... ���� ������ �� ���ƿ�.";
                break;

            case 1:
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "��, ���� ���̴� �� �����ΰ���?";
                break;

            case 2:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[6];
                helpText.text = "�׷�, ���Ͱ� �� ���� ���ƴٴϰ� �ִµ� ������ �̿��ϸ� ���� ���� �� ���� �Ŷ�� �ϴ´�.";
                break;

            case 3:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[7];
                helpText.text = "�׸��� ���߿� ���� �����ִٸ� �ֺ� �繰�� ���� �ֹ��� �̿��ؼ� ������ �� �ֵ��� ����غ���.";
                break;

            case 4:
                image.sprite = spriteArr[0];
                image2.sprite = spriteArr[6];
                helpText.text = "���� ���ʿ��� ��ٸ��� �����״�, �� �� ȥ�� �ذ��غ��Ŷ�.";
                break;

            case 5:
                image.sprite = spriteArr[4];
                image2.sprite = spriteArr[0];
                helpText.text = "��, ���´�!";
                break;

            case 6:
                helpPanel.SetActive(false);
                helpActive = false;
                Time.timeScale = 1f;
                startScript = true;
                break;

            default:
                Debug.Log("Start Script : " + clickCount);  // ����׿�
                break;
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
                image2.sprite = spriteArr[0];
                helpText.text = "A, D Ű�� ������ �� �ְ�, \nSpaceBar�� ������ �� �־�. \n �ϴ� ���� �ѷ�����?";
            } else if(collider.name == "HelpAreaFire")
            {
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "��? ȶ���� �ֳ�?\n��Ŭ������ �������� �������� ����� �� ���� �� ����!";
                // ���� ó�� ������� �߰�
            }
            else if(collider.name == "HelpAreaEnemy")
            {
                image.sprite = spriteArr[2];
                image2.sprite = spriteArr[0];
                helpText.text = "��, ���� �����ΰ�? ����ġ�ų� �����ؼ� �������߰ھ�...\n��Ƶ� ������ ����� ���� �� ����.";
            } else if(collider.name == "HelpAreaMagic")
            {
                image.sprite = spriteArr[1];
                image2.sprite = spriteArr[0];
                helpText.text = "�������̴�! ������ ���� Q Ű�� ���� �������� ���ĺ���?";
                // ������ ó�� ������ ��/�ذ������� �߰�
            } else if(collider.name == "HelpAreaBar")
            {
                image.sprite = spriteArr[3];
                image2.sprite = spriteArr[0];
                helpText.text = "���� �����ֳ�... �� ������ ������ ������ �� ���� �� ������?\n�� ������ �������� �̿��ϸ� �������� �� �� ���� ������...?";
            }
            else if (collider.name == "HelpAreaStair")
            {
                image.sprite = spriteArr[3];
                image2.sprite = spriteArr[0];
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
