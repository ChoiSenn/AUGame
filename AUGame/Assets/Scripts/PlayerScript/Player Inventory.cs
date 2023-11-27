using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("인벤토리")]
    public Inventory inventory;

    public AudioSource audioSource;
    public AudioClip GetSound;

    public ParticleSystem MagicEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // 클릭해서 마법서에 담는 코드
        {
            Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // RaycaseHit2D가 클릭된 곳에 오브젝트가 있나 체크
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector4.zero);

            if (hit.collider != null)
            { // 오브젝트를 클릭했다면 HitCheckObject(hit) 함수로 hit 정보를 넘김
                Debug.Log(hit.collider.name);
                HitCheckObject(hit);
            }
        }
    }

    void HitCheckObject(RaycastHit2D hit)
    {
        // 클릭된 오브젝트의 IObjectItem 인터페이스를 clickInterface에 넘김
        IObjectItem clickInterface = hit.transform.gameObject.GetComponent<IObjectItem>();

        if (clickInterface != null) // clickInterface가 인터페이스를 가지고 있을 시.
        {
            ParticleSystem magicParticleSystem = Instantiate(MagicEffect);
            magicParticleSystem.transform.position = hit.transform.position;
            magicParticleSystem.Play();

            Item item = clickInterface.ClickItem(); // item에 클릭된 오브젝트의 아이템 정보를 넘김
            Debug.Log($"{item.itemName}");
            inventory.AddItem(item);
            audioSource.PlayOneShot(GetSound);
        }
    }
}