using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBatAI : MonoBehaviour
{
    public Transform target;

    Enemy enemy;
    Animator enemyAnimator;
    Rigidbody2D rigid;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine("Pattern1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(0.5f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 100, 0 * 100);

        StartCoroutine("Pattern2");
    }

    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(3.0f);

        transform.localScale = new Vector3(-200, 200, 1);
        rigid.velocity = new Vector2(1 * 100, 0 * 100);

        StartCoroutine("Pattern3");
    }

    IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(3.0f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(-1 * 100, -1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(1 * 100, 1 * 100);

        yield return new WaitForSeconds(0.3f);

        rigid.velocity = new Vector2(-1 * 100, -1 * 100);

        StartCoroutine("Pattern4");
    }

    IEnumerator Pattern4()
    {
        yield return new WaitForSeconds(0.3f);

        transform.localScale = new Vector3(200, 200, 1);
        rigid.velocity = new Vector2(-1 * 500, 0 * 500);

        StartCoroutine("Pattern6");
    }

    IEnumerator Pattern5()
    {
        yield return new WaitForSeconds(1.0f);

        transform.localScale = new Vector3(-200, 200, 1);
        // Åº¸· ¹ß»ç

        //StartCoroutine("Pattern1");
    }

    IEnumerator Pattern6()
    {
        yield return new WaitForSeconds(1.0f);

        transform.localScale = new Vector3(-200, 200, 1);

        rigid.velocity = new Vector2(1 * 200, -0.8f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.7f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.6f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.5f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.4f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.3f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.2f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, -0.1f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.1f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.2f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.3f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.4f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.5f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.6f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.7f * 300);
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = new Vector2(1 * 200, 0.8f * 300);
        yield return new WaitForSeconds(0.1f);

        //StartCoroutine("Pattern6");
    }

}
