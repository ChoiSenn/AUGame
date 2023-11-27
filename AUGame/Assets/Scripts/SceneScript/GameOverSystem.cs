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

    public void Retry()  // Retry 버튼 눌렸을 때, 해당 스테이지 재시작 (일단은 T1 스테이지에서만 게임오버가 가능하므로 이쪽으로 가게 해놨는데 나중에는 삭제되지 않는 GameObject를 이용해 데이터를 담아둬야 함)
    {
        Application.LoadLevel("Theory1Stage");
    }

    public void Menu()  // Menu 버튼 눌렸을 때, 스테이지 선택 화면으로 이동
    {
        Application.LoadLevel("StageMenu");
    }

    public void Exit()  // Exit 버튼 눌렸을 때, 게임 종료
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
