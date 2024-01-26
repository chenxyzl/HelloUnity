using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ToolbarSlotUI> slotUIList;
    private ToolbarSlotUI selectSlotUI;
    private void Awake()
    {
    }
    void Start()
    {
        InitUI();
    }
    void InitUI()
    {
        slotUIList = new List<ToolbarSlotUI>(new ToolbarSlotUI[9]);
        ToolbarSlotUI[] slotUIArray = transform.GetComponentsInChildren<ToolbarSlotUI>();
        foreach (var item in slotUIArray)
        {
            slotUIList[item.index] = item;
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        List<SlotData> slotDataList = InventoryManager.Instance.toolbarData.slotList;
        for (var i = 0; i < slotDataList.Count; i++)
        {
            slotUIList[i].SetData(slotDataList[i]);
        }
    }
    // Update is called once per frame
    void Update() 
    {
        //check 锄头的使用
        ToolbarSelectControl();
    }

    public ToolbarSlotUI GetSelectToolbarSlotUI()
    {
        return selectSlotUI;
    }

    void ToolbarSelectControl()
    {
        for (var i = KeyCode.Alpha1; i <= KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown(i))
            {
                if (selectSlotUI != null)
                {
                    selectSlotUI.UnHightlight();
                }
                var index = i - KeyCode.Alpha1;
                selectSlotUI = slotUIList[index];
                selectSlotUI.Hightlight();
            }
        }
    }
}
