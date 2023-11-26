using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items; // 아이템을 담을 리스트

    [SerializeField]
    private Transform slotParent; // Slot의 부모가 되는 Bag을 담을 곳
    [SerializeField]
    private Slot[] slots; // Bag의 하위에 등록된 Slot을 담을 곳

#if UNITY_EDITOR
    private void OnValidate() // 유니티 에디터에서 바로 작동을 하는 역할
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake() // 게임이 시작되면 items에 들어 있는 아이템을 인벤토리에 삽입
    {
        FreshSlot();
    }

    public void FreshSlot()
    { // 아이템이 들어오거나 나가면 Slot의 내용을 다시 정리하여 화면에 보여 주는 기능
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

    public void AddItem(Item _item) // 아이템을 획득할 경우
    {
        items.Insert(0, _item);

        FreshSlot();
    }
}