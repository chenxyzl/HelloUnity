using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class SlotData
{
    public ItemData item;
    public int count = 0;
    private Action OnChange;

    public  bool IsEmpty()
    {
        return count == 0;
    }

    public bool CanAddItem()
    {
        return item.maxCount > count;
    }
    public void SwithFrom(SlotData data)
    {
        this.item = data.item;
        this.count = data.count;
        OnChange?.Invoke();
    }
    public void AddOne()
    {
        count++;
        OnChange?.Invoke();
    }
    public void ReduceOne()
    {
        count--;
        if (count == 0)
        {
            item = null;
        }
        OnChange?.Invoke();
        
    }
    public void AddItem(ItemData item)
    {
        this.item = item;
        AddOne();
    }
    public void Clear()
    {
        item = null;
        count = 0;
        OnChange?.Invoke();
    }
    public void AddListener(Action OnChange)
    {
        this.OnChange = OnChange;
    }
}
