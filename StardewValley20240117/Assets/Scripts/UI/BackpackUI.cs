using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
    private GameObject parentUI;
    public List<SlotUI> slotUIList;
    // Start is called before the first frame update
    private void Awake()
    {
        parentUI = transform.Find("ParentUI").gameObject;
        //parentUI.SetActive(false);
    }
    private void Start()
    {
        InitUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }
    void InitUI()
    {
        slotUIList = new List<SlotUI>(new SlotUI[21]);
        SlotUI[] slotUIArray = transform.GetComponentsInChildren<SlotUI>();
        foreach (var item in slotUIArray)
        {
            slotUIList[item.index] = item;
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        List<SlotData> slotDataList = InventoryManager.Instance.backpack.slotList; 
        for (int i = 0; i < slotDataList.Count; i++)
        {
            slotUIList[i].SetData(slotDataList[i]);
        }
    }
    private void ToggleUI()
    {
        parentUI.SetActive(!parentUI.activeSelf);
    }
    public void OnCloseClick()
    {
        ToggleUI();
    }
}
