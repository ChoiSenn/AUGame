using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollScript2 : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� ���� ������ �����ϵ��� static
    public static bool GameIsPaused = false;  // ���� �����ϰ� ���� �� ���
    public GameObject MagicScrollCanvas2;

    public InputField CodeInput1;  // �÷��̾ �ۼ��� �ڵ�
    public InputField CodeInput2;  // �÷��̾ �ۼ��� �ڵ�2
    public InputField CodeInput3;  // �÷��̾ �ۼ��� �ڵ�3
    public GameObject Block;  // �ڵ� �Է� �� ������ ��
    public Text ErrorText;  // ���� ��� ���� �ؽ�Ʈ
    public Text HintText;  // ��Ʈ ��� ���� �ؽ�Ʈ

    public int forCount;  // for�� ���� ����

    public UIInventoryClick uiInven;
    public bool BlindOut = false;  // Bar�� ���� �ڸ��� �־� ����ε尡 ���������
    public GameObject BlindBtn;

    public PlayerMoving playerMoving;

    public int failCount = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Resume()
    {
        MagicScrollCanvas2.SetActive(false);
    }

    public void Pause()
    {
        MagicScrollCanvas2.SetActive(true);
    }

    public void CompileButton()  // ������ ��ư
    {
        // Debug.Log(CodeInput.text);  // InputField�� �ۼ��� �ؽ�Ʈ�� �о��
        forCount = int.Parse(CodeInput1.text);
        string code1 = CodeInput2.text.Replace(" ", string.Empty);
        string code2 = CodeInput3.text.Replace(" ", string.Empty);
        string errorMessage = "���� �޽��� ����";
        string hint = "��Ʈ ����";

        if (code1 != "j>=i")  // �� ��° ��ĭ�� �ڵ尡 Ʋ���� ���
        {
            errorMessage = "for���� ù ��° �� ĭ�� �ڵ尡 Ʋ����. ��� ���¸� ����� ���ؼ��� � ������� ������ ���� ���� �����غ�����!";
            ErrorText.text = errorMessage;
            HintText.text = hint;
            failCount += 1;
        }
        else if (code2 != "k<=i")  // �� ��° ��ĭ�� �ڵ尡 Ʋ���� ���
        {
            errorMessage = "for���� �� ��° �� ĭ�� �ڵ尡 Ʋ����. ��� ���¸� ����� ���ؼ��� � ������� ���� ���� ���� �����غ�����!";
            ErrorText.text = errorMessage;
            HintText.text = hint;
            failCount += 1;
        } else  // �� �� �¾�����
        {
            Debug.Log("��� ���� ��� : " + forCount);

            Time.timeScale = 1f;  // �ð� �帣�� �����
            MagicScrollCanvas2.SetActive(false);  // ������ũ�� â �ݰ�
            playerMoving.MagicScrollCanvasFlag = false;

            Invoke("BlockStair", 1);
        }

        if (failCount == 1)  // n�� �̻� ���� ������ Ʋ���� ��� �߰� ��Ʈ ���
        {
            hint = "hint 1. ������ ��� �������� �������� � �ڵ带 �ۼ��ؾ� ���� �����غ���.";
            HintText.text = hint;
        }
        else if (failCount >= 2)
        {
            hint = "hint 1. ������ ��� �������� �������� � �ڵ带 �ۼ��ؾ� ���� �����غ���.\nhint.2 ù ��° ĭ�� ������ ����ϴ� �ݺ����̰�, �� ��° ĭ�� ������ ����ϴ� �ݺ����ӿ� �ָ�����.";
            HintText.text = hint;
        }
        //else if (failCount >= 3)
        //{
        //    hint = "hint 1. ������ ��� �������� �������� � �ڵ带 �ۼ��ؾ� ���� �����غ���.\nhint 2. ù ��° ĭ�� ������ ����ϴ� �ݺ����̰�, �� ��° ĭ�� ������ ����ϴ� �ݺ����ӿ� �ָ�����.\nhint 3. ù ��° ĭ�� .";
        //    HintText.text = hint;
        //}
        else
        {
            HintText.text = hint;
        }
    }

    void BlockStair()
    {
        BlockStairStart(forCount);  // �� �ױ�
    }

    public GameObject explosion;

    void BlockStairStart(int Count)
    {
        if (Count >= 0)
        {
            int blockX = 1866;

            for (int i = 0; i < Count; i++)
            {
                Instantiate(Block, new Vector3(blockX, 300, 0), Quaternion.identity);
                blockX -= 19;
            }

            forCount = Count - 1;
            Invoke("BlockStair", 0.2f);  // ����ؼ� ��� ������� �� �ױ�
        }
    }

    public void ResetButton()
    {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("CreateBlock");

        Debug.Log(temp.Length + "�� ã��");

        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
    }

    public void BlindButton()
    {
        Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (uiInven.nowMagic == "Block Item")
        {
            Destroy(BlindBtn);
            BlindOut = true;
        }
        else
        {
            ErrorText.text = "�̵����Ѿ� �� ��ü�� Ʋ�Ƚ��ϴ�!";
        }
    }
}
