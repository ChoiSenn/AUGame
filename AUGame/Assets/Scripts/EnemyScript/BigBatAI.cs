using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBatAI : MonoBehaviour
{
    public Transform target;

    Enemy enemy;
    Animator enemyAnimator;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;

        StartCoroutine("Pattern1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(1.0f);
        transform.localScale = new Vector3(-200, 200, 1);

        StartCoroutine("Pattern2");
    }

    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(1.0f);
        transform.localScale = new Vector3(200, 200, 1);

        StartCoroutine("Pattern1");
    }

}
