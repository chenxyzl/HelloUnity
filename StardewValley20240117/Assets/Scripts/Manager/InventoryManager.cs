using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private Dictionary<ItemType, ItemData> itemDataDict = new Dictionary<ItemType, ItemData>();
    [HideInInspector]
    public InventoryData backpack;
    [HideInInspector]
    public InventoryData toolbarData;
    private void Awake()
    {
        Instance = this;
        Init();
    }
    private void Init()
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("Data");
        foreach (var item in itemDataArray)
        {
            itemDataDict.Add(item.type, item);
        }
        backpack = Resources.Load<InventoryData>("Data/Backpack");
        toolbarData = Resources.Load<InventoryData>("Data/Toolbar");
        Debug.Log(backpack);
    }

    private ItemData GetItemData(ItemType type)
    {
        bool exist = itemDataDict.TryGetValue(type, out var itemData);
        if (!exist)
        {
            Debug.LogWarning("你传递的type: " + type + " 不存在");
        }
        return itemData;
    }

    public void AddToBackpack(ItemType type)
    {
        ItemData item = GetItemData(type);
        if (item == null)
        {
            return;
        }
        foreach (var slotData in backpack.slotList)
        {
            if (slotData.item != null && slotData.item.type == item.type && slotData.CanAddItem())
            {
                slotData.AddOne();
                return;
            }
        }
        foreach (var slotData in backpack.slotList)
        {
            if (slotData.item == null || slotData.count == 0)
            {

                slotData.AddItem(item);
                return;
            }
        }

        Debug.LogWarning("无法放入仓库,背包"+backpack+"已满");
    }
}
