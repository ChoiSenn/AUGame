using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("�κ��丮")]
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
        if (Input.GetMouseButtonDown(1))  // Ŭ���ؼ� �������� ��� �ڵ�
        {
            Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // RaycaseHit2D�� Ŭ���� ���� ������Ʈ�� �ֳ� üũ
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector4.zero);

            if (hit.collider != null)
            { // ������Ʈ�� Ŭ���ߴٸ� HitCheckObject(hit) �Լ��� hit ������ �ѱ�
                Debug.Log(hit.collider.name);
                HitCheckObject(hit);
            }
        }
    }

    void HitCheckObject(RaycastHit2D hit)
    {
        // Ŭ���� ������Ʈ�� IObjectItem �������̽��� clickInterface�� �ѱ�
        IObjectItem clickInterface = hit.transform.gameObject.GetComponent<IObjectItem>();

        if (clickInterface != null) // clickInterface�� �������̽��� ������ ���� ��.
        {
            ParticleSystem magicParticleSystem = Instantiate(MagicEffect);
            magicParticleSystem.transform.position = hit.transform.position;
            magicParticleSystem.Play();

            Item item = clickInterface.ClickItem(); // item�� Ŭ���� ������Ʈ�� ������ ������ �ѱ�
            Debug.Log($"{item.itemName}");
            inventory.AddItem(item);
            audioSource.PlayOneShot(GetSound);
        }
    }
}