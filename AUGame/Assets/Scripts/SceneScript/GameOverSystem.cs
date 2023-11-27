using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()  // Retry ��ư ������ ��, �ش� �������� ����� (�ϴ��� T1 �������������� ���ӿ����� �����ϹǷ� �������� ���� �س��µ� ���߿��� �������� �ʴ� GameObject�� �̿��� �����͸� ��Ƶ־� ��)
    {
        Application.LoadLevel("Theory1Stage");
    }

    public void Menu()  // Menu ��ư ������ ��, �������� ���� ȭ������ �̵�
    {
        Application.LoadLevel("StageMenu");
    }

    public void Exit()  // Exit ��ư ������ ��, ���� ����
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
