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

    public void StartBtn()  // Start 버튼 눌렸을 때, 스테이지 선택 화면으로 이동
    {
        Application.LoadLevel("StageMenu");
    }

    public void SettingBtn()  // Setting 버튼 눌렸을 때, 아직 작동하지 않음 (TODO:)
    {
        // Application.LoadLevel("Setting");
    }

    public void ExitBtn()  // Exit 버튼 눌렸을 때, 게임 종료
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
