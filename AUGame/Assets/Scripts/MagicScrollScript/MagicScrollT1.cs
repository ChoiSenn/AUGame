using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicScrollT1 : MonoBehaviour
{
    // �ٸ� ��ũ��Ʈ���� ���� ������ �����ϵ��� static
    public static bool GameIsPaused = false;  // ���� �����ϰ� ���� �� ���
    public GameObject MagicScrollCanvas;

    public InputField CodeInput;  // �÷��̾ �ۼ��� �ڵ�
    public Text ErrorText;  // ���� ��� ���� �ؽ�Ʈ
    public Text HintText;  // ��Ʈ ��� ���� �ؽ�Ʈ

    public GameObject Block;  // ���� �� ����� ��� (TODO : ������ ����)
    public GameObject Block2;
    public GameObject Block3;
    public GameObject Block4;
    public GameObject Block5;

    public PlayerMoving playerMoving;
    public int failCount = 0;

    public AudioSource audioSource;
    public AudioClip doorOpen;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void Resume()
    {
        MagicScrollCanvas.SetActive(false);
    }

    public void Pause()
    {
        MagicScrollCanvas.SetActive(true);
    }

    public void CompileButton()  // ������ ��ư
    {
        // Debug.Log(CodeInput.text);  // InputField�� �ۼ��� �ؽ�Ʈ�� �о��
        string code = CodeInput.text;
        string errorMessage = "���� �޽��� ����";
        string hint = "��Ʈ ����";

        if (code == "open the door")
        {
            errorMessage = " �����Դϴ� !!";
            ErrorText.text = errorMessage;

            OpenDoor();
        }
        else
        {
             errorMessage = " Ʋ�Ƚ��ϴ� !!";
             failCount += 1;

            if (failCount == 1)  // n�� �̻� ���� ������ Ʋ���� ��� �߰� ��Ʈ ���
            {
                hint = "hint 1. a[3:6]�� ���ڿ� a�� 3��° �ڸ����� 6��°�� �ڸ����� �ڸ��ٴ� ���̴�.";
            }
            else if (failCount == 2)
            {
                hint = "hint 1. a[3:6]�� ���ڿ� a�� 3��° �ڸ����� 6��°�� �ڸ����� �ڸ��ٴ� ���̴�.\nhint 2. %s�� % ���� �߰��� ���ڸ� ���ڿ��� �ش� �ڸ��� �����Ѵٴ� ���̴�.";
            }
            else if (failCount >= 3)
            {
                hint = "hint 1. a[3:6]�� ���ڿ� a�� 3��° �ڸ����� 6��°�� �ڸ����� �ڸ��ٴ� ���̴�.\nhint 2. %s�� % ���� �߰��� ���ڸ� ���ڿ��� �ش� �ڸ��� �����Ѵٴ� ���̴�.\nhint 3. ���̽㿡���� '+'�� ���ڿ��� �̾���� �� �ִ�.";
            }

            ErrorText.text = errorMessage;
            HintText.text = hint;
        }
    }

    public GameObject explosion;

    void OpenDoor()
    {
        Time.timeScale = 1f;  // �ð� �帣�� �����
        MagicScrollCanvas.SetActive(false);  // ������ũ�� â �ݰ�
        playerMoving.MagicScrollCanvasFlag = false;
        audioSource.PlayOneShot(doorOpen);

        var explo = Instantiate(explosion, Block.transform.position, Quaternion.identity);
        var explo2 = Instantiate(explosion, Block2.transform.position, Quaternion.identity);
        var explo3 = Instantiate(explosion, Block3.transform.position, Quaternion.identity);
        var explo4 = Instantiate(explosion, Block4.transform.position, Quaternion.identity);
        var explo5 = Instantiate(explosion, Block5.transform.position, Quaternion.identity);
        Destroy(Block, 0.1f);
        Destroy(Block2, 0.1f);
        Destroy(Block3, 0.1f);
        Destroy(Block4, 0.1f);
        Destroy(Block5, 0.1f);
        Destroy(explo, 0.5f);
        Destroy(explo2, 0.5f);
        Destroy(explo3, 0.5f);
        Destroy(explo4, 0.5f);
        Destroy(explo5, 0.5f);
    }

}
