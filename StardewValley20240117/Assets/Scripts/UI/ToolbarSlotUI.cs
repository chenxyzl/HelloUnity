using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlotUI : SlotUI
{
    // Start is called before the first frame update
    public Sprite slotLight;
    public Sprite slotDark;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Hightlight()
    {
        image.sprite = slotDark;
    }
    public void UnHightlight() {
        image.sprite = slotLight;
    }
}
