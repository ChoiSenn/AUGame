using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtn()  // Start ��ư ������ ��, �������� ���� ȭ������ �̵�
    {
        Application.LoadLevel("StageMenu");
    }

    public void SettingBtn()  // Setting ��ư ������ ��, ���� �۵����� ���� (TODO:)
    {
        // Application.LoadLevel("Setting");
    }

    public void ExitBtn()  // Exit ��ư ������ ��, ���� ����
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
