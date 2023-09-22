using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class fireMagic : MonoBehaviour
{
    public float speed; //스피드 선언
    public float distance;
    public LayerMask isLayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyFire", 2); // 일정 시간 후 파괴 = Invoke("실행 할 함수명", 지연 시간)
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (ray.collider != null)
        {
            if (ray.collider.tag == "Enemy") // 태그 Enemy 생성, 슬라임에게 할당 후 사용.
            {
                Debug.Log("명중!");

            }
            DestroyFire();
        }

        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime); // 오른쪽으로 생성된 fire 클론 발사
        }
        else 
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime); // 왼쪽으로 생성된 fire 클론 발사
        }
    }

    void DestroyFire()
    {
        //Destroy(gameObject); // Destroy(파괴할 오브젝트)
    }
}