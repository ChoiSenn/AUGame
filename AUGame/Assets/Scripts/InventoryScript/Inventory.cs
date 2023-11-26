using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items; // �������� ���� ����Ʈ

    [SerializeField]
    private Transform slotParent; // Slot�� �θ� �Ǵ� Bag�� ���� ��
    [SerializeField]
    private Slot[] slots; // Bag�� ������ ��ϵ� Slot�� ���� ��

#if UNITY_EDITOR
    private void OnValidate() // ����Ƽ �����Ϳ��� �ٷ� �۵��� �ϴ� ����
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake() // ������ ���۵Ǹ� items�� ��� �ִ� �������� �κ��丮�� ����
    {
        FreshSlot();
    }

    public void FreshSlot()
    { // �������� �����ų� ������ Slot�� ������ �ٽ� �����Ͽ� ȭ�鿡 ���� �ִ� ���
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item) // �������� ȹ���� ���
    {
        items.Insert(0, _item);

        FreshSlot();
    }
}