using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoBehaviour
{
    public static ItemMoveHandler instance;
    private Image icon;
    private SlotData selectedSlotData;
    private Player player;
    private bool isCtrlDown = false;
    private void Awake()
    {
        instance = this;
        icon = GetComponentInChildren<Image>();
        player = GameObject.FindAnyObjectByType<Player>();
        HideIcon();
    }

    public void OnSlotClick(SlotUI slotui)
    {
        print("OnSlotClick:" + slotui); 
        
        //手里不是空,交换
        if (selectedSlotData != null)
        {
            SwitchItem(selectedSlotData, slotui.GetData());
            ClearHand(); 
            return;
        }
        //手里是空 点的也是空跳过
        if(slotui.GetData() ==null || slotui.GetData().IsEmpty())
        {
            return;
        }
        //手里是空 点的不是空 选择
        selectedSlotData = slotui.GetData();
        ShowIcon(slotui.GetData().item.sprite);
    }

    void HideIcon()
    {
        icon.enabled = false;
    }
    void ShowIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    private void Update()
    {
        if (icon.enabled)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                Input.mousePosition,
                null,
                out position);
            icon.GetComponent<RectTransform>().anchoredPosition = position;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (icon.enabled)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    ThrowItem();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (icon.enabled)
            {
                ClearHand();    
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCtrlDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCtrlDown = false;
        }
    }

    private void ClearHand()
    {
        HideIcon();
        selectedSlotData = null;
    }

    private void ThrowItem()
    {
        if (selectedSlotData == null || selectedSlotData.item == null)
        {
            return;
        }
        if (isCtrlDown && selectedSlotData.count > 1)
        {
            GameObject prefab = selectedSlotData.item.prefab;
            player.ThrowItem(prefab, 1);
            selectedSlotData.ReduceOne();
        }
        else
        {
            GameObject prefab = selectedSlotData.item.prefab;
            player.ThrowItem(prefab, selectedSlotData.count);
            selectedSlotData.Clear();
            ClearHand();
        }

    }

    private void SwitchItem(SlotData fromDat, SlotData toData)
    {
        var item = toData.item;
        var count = toData.count;
        toData.SwithFrom(fromDat);
        fromDat.SwithFrom(new SlotData { item = item, count = count });
    }
}
