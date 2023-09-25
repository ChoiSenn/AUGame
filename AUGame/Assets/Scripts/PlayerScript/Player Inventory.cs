using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("�κ��丮")]
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // Ŭ���ؼ� �������� ��� �ڵ�
        {
            Debug.Log(inventory);

            Vector4 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // RaycaseHit2D�� Ŭ���� ���� ������Ʈ�� �ֳ� üũ
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector4.zero);

            if (hit.collider != null)
            { // ������Ʈ�� Ŭ���ߴٸ� HitCheckObject(hit) �Լ��� hit ������ �ѱ�
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
            Item item = clickInterface.ClickItem(); // item�� Ŭ���� ������Ʈ�� ������ ������ �ѱ�
            print($"{item.itemName}");
            inventory.AddItem(item);
        }
    }
}