using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageClear: MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D Stair)  // �ش� �±׿� ����� �� 
    {
        if (Stair.CompareTag("Player"))
        {
            Destroy(gameObject);
            Application.LoadLevel("StageMenu");
        }
    }
}