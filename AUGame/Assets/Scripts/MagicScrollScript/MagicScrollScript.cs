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

    void Update()
    {
        
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void CompileButton()  // ������ ��ư
    {
        Debug.Log("������!!");
        Debug.Log(CodeInput.text);  // InputField�� �ۼ��� �ؽ�Ʈ�� �о��
    }
}
