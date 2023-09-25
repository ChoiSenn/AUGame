using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)  // UI �κ��丮 Ŭ�� ��
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedObject);
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