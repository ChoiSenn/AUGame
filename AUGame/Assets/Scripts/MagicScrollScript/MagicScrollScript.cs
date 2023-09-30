using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollScript : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� ���� ������ �����ϵ��� static
    public static bool GameIsPaused = false;  // ���� �����ϰ� ���� �� ���
    public GameObject MagicScrollCanvas;

    public InputField CodeInput;  // �÷��̾ �ۼ��� �ڵ�
    public GameObject Bar;  // �ڵ� �Է� �� �������� ��
    public Text ErrorText;  // ���� ��� ���� �ؽ�Ʈ
    public Text HintText;  // ��Ʈ ��� ���� �ؽ�Ʈ

    public UIInventoryClick uiInven;
    public bool BlindOut = false;  // Bar�� ���� �ڸ��� �־� ����ε尡 ���������
    public GameObject BlindBtn;

    void Update()
    {
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void CompileButton()  // ������ ��ư
    {
        // Debug.Log(CodeInput.text);  // InputField�� �ۼ��� �ؽ�Ʈ�� �о��
        string code = CodeInput.text;
        string errorMessage = "���� �޽��� ����";
        string hint = "��Ʈ ����";
        int Bar_Y_Position = 0;

        if(BlindOut == false)
        {
            errorMessage = "���� �Ҵ��� �߸��Ǿ���.";
            hint = "�����̰��� �ϴ� �繰�� ����� ĭ�� �Ҵ��ߴ��� Ȯ���ϼ���!";
        }
        else if(code.Length <= 14 || code.Substring(0, 14) != "Bar_Y_Position")  // �ڵ� ���̰� ª�ų� �տ� Bar_Y_Position�� �ƴϸ� ����
        {
            errorMessage = "�����ϰ��� �ϴ� ������ ���� �߸��Ǿ���";
            hint = "� ���� �����ؾ� �ϴ��� Ȯ���ϼ���!";
        } else if(code.Substring(code.Length-1) != ";")  // �������� ;�� �� ������
        {
            errorMessage = "���� ����!";
            hint = "�������� ;�� �ٿ����� Ȯ���ϼ���!";
        }
        else
        {
            code = code.Substring(14);  // �����ϰ��� �ϴ� Bar_Y_Position �����ϰ�
            code = code.Substring(0, code.Length-1);  // ; ����
            code = code.Replace(" ", string.Empty);  // ��������
            //string[] codeWords = code.Split();  // �������� �߶� �ذ��ұ� �ߴµ� �� ���� ��쵵 ������
            //for(int i = 0; i < codeWords.Length; i++)
            //{
            //    Debug.Log(codeWords[i]);
            //}
            if (code.Substring(0, 2) == "-=")  // -=�� ��쿡
            {
                code = code.Substring(2);
                Bar_Y_Position = int.Parse(code);
            } else if (code.Substring(0, 16) == "=Bar_Y_Position-")  // = �� ��쿡
            {
                code = code.Substring(16);
                Bar_Y_Position = int.Parse(code);
            }
            else
            {
                errorMessage = "���� ����!";
                hint = "�ڵ带 �ٽ� �� �� Ȯ���ϼ���!";
            }
        }

        if (Bar_Y_Position != 0)  // 0�� �ƴ� ��� �� �̵�
        {
            BarMoving(Bar_Y_Position);
        }
        else
        {
            ErrorText.text = errorMessage;
            HintText.text = hint;
        }
    }

    public GameObject explosion;

    void BarMoving(int bar_Y_Position)
    {
        MagicScrollCanvas.SetActive(false);

        //Debug.Log("���� : " + bar_Y_Position);
        var explo = Instantiate(explosion, Bar.transform.position, Quaternion.identity);
        Destroy(explo, 0.5f);

        Vector3 destination = new Vector3(Bar.transform.position.x, -(16000 *bar_Y_Position), 0);
        Vector3 speed = Vector3.zero; // (0,0,0) �� .zero �ε� ǥ������
        Bar.transform.position = Vector3.SmoothDamp(Bar.transform.position, destination, ref speed, 0.1f);
    }

    public void ResetButton()
    {
        Bar.transform.position = new Vector3(1251, 55, 0);
    }

    public void BlindButton()
    {
        Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (uiInven.nowMagic == "Bar Item")
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
