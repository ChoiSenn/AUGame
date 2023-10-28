using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryClick : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    Inventory inventory;
    public Slot slot1;
    public Slot slot2;
    public Slot slot3;
    public GameObject slotOutline1;
    public GameObject slotOutline2;
    public GameObject slotOutline3;
    Image slotImage1;
    Image slotImage2;
    Image slotImage3;

    public string nowMagic = "";

    void Start()
    {
        inventory = GetComponent<Inventory>();
        slotImage1 = slotOutline1.GetComponent<Image>();
        slotImage2 = slotOutline2.GetComponent<Image>();
        slotImage3 = slotOutline3.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)  // UI 인벤토리 클릭 시
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;  // 클릭 된 마법 UI 부분 확인

        Debug.Log(clickedObject);

        if (eventData.button == PointerEventData.InputButton.Right)  // 우클릭이면 마법 사용
        {
            if (clickedObject == slot1.gameObject)  // 1번 슬롯에 들어있는 마법 확인
            {
                nowMagic = slot1.item.itemName;
                slotImage1.color = Color.grey;
                slotImage2.color = Color.white;
                slotImage3.color = Color.white;
            }
            else if (clickedObject == slot2.gameObject)
            {
                nowMagic = slot2.item.itemName;
                slotImage2.color = Color.grey;
                slotImage1.color = Color.white;
                slotImage3.color = Color.white;
            }
            else if (clickedObject == slot3.gameObject)
            {
                nowMagic = slot3.item.itemName;
                slotImage3.color = Color.gray;
                slotImage1.color = Color.white;
                slotImage2.color = Color.white;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
    }
}