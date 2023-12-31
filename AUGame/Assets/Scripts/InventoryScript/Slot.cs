using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image; // Image Component를 담을 곳

    private Item _item;
    public Item item
    {
        get { return _item; } // 슬롯의 item 정보를 넘겨줄 때 사용
        set
        {
            _item = value; // item에 들어오는 정보의 값은 _item에 저장
            if (_item != null)
            { // List<Item> items에 등록된 아이템이 있다면 itemImage를 image에 저장 
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1); // Image의 알파 값을 1로 하여 이미지를 표시
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            } // item이 null 이면 (빈슬롯 이면) Image의 알파 값 0을 주어 화면에 미표시
        }
    }
}