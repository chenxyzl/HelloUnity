using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu()]
public class InventoryData:ScriptableObject
{
    public List<SlotData> slotList;
}
