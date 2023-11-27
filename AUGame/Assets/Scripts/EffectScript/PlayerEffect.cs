using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public ParticleSystem jumpDust;
    public ParticleSystem walkDust;
    public bool isJump = false;
    public PlayerMoving player;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("JumpEffect");
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) // A키와 대쉬키를 눌렀을 때 워크 이펙트 실행
        {
            StartCoroutine("walkDustEffect");
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) // D키와 대쉬키를 눌렀을 때 워크 이펙트 실행
        {
            StartCoroutine("walkDustEffect");
        }
    }

    public IEnumerator JumpEffect()  // 점프할 때 이펙트 코루틴
    {
        ParticleSystem jumpParticleSystem = Instantiate(jumpDust);
        jumpParticleSystem.transform.position = new Vector3(player.posPlayer.position.x, player.posPlayer.position.y-30);
        jumpParticleSystem.Play();

        yield return new WaitForSeconds(0.001f);
        jumpParticleSystem.Stop();
    }

    public IEnumerator walkDustEffect()  // 걸을 때 이펙트 코루틴
    {
        ParticleSystem walkParticleSystem = Instantiate(walkDust);
        walkParticleSystem.transform.position = new Vector3(player.posPlayer.position.x, player.posPlayer.position.y);
        walkParticleSystem.Play();

        yield return new WaitForSeconds(0.001f);
        walkParticleSystem.Stop();
    }
}
