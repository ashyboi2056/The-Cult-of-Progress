using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class UI_ISlot : DEBUGMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // ENTIRELY AI MADE MAY BREAK!!!!!

    [Label("Slot ID")] public int slotIndex;
    public Image iconImage;
    private PLAYER_Inventory inventory;
    private Transform originalParent;
    private GameObject placeholder;
    public ISlotType slotType;

    void Awake()
    {
        inventory = FindFirstObjectByType<LOCAL_PLAYER_FLAG>().gameObject.GetComponent<PLAYER_Inventory>();
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        var item = inventory.GetItem(slotIndex);
        iconImage.sprite = item != null ? item.STAT_icon : null;
        iconImage.enabled = item != null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory == null || inventory.GetItem(slotIndex) == null) return;

        eventData.pointerDrag = gameObject;

        originalParent = iconImage.transform.parent;

        placeholder = new GameObject("Placeholder");
        placeholder.transform.SetParent(originalParent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = iconImage.rectTransform.sizeDelta.x;
        le.preferredHeight = iconImage.rectTransform.sizeDelta.y;

        iconImage.transform.SetParent(transform.root);

        CanvasGroup cg = iconImage.GetComponent<CanvasGroup>();
        if (cg == null) cg = iconImage.gameObject.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(originalParent);
        iconImage.rectTransform.anchoredPosition = Vector2.zero;

        Destroy(placeholder);

        // Restore raycast blocking
        iconImage.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen position to local position in the root canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.root as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        iconImage.rectTransform.localPosition = localPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag.GetComponent<UI_ISlot>();

        //Cant Drop in this slot!!!
        if (!QueryCanDrop(inventory.GetItem(draggedSlot.slotIndex)))
        {
            // Return to origin
            return;
        }

        else if (draggedSlot != null && draggedSlot.slotIndex != slotIndex)
        {
            // Swap items
            inventory.SwapItems(slotIndex,draggedSlot.slotIndex);

            UpdateSlot();
            draggedSlot.UpdateSlot();
        }
    }

    public bool QueryCanDrop(soDATA_ITEM item)
    {
        if (item is soDATA_ITEM_Usable usableItem)
        {
            if (slotType == ISlotType.Use){ return true; }
            if (slotType == ISlotType.Any){ return true; }
        }
        else if (item is soDATA_ITEM_Accessory accItem)
        {
            if (slotType == ISlotType.Acc){ return true; }
            if (slotType == ISlotType.Any){ return true; }
        }
        else if (item is soDATA_ITEM genericItem)
        {
            if (slotType == ISlotType.Any){ return true; }
        }

        return false;
    }

    public static void UpdateAllUIISlots()
    {
        foreach (var instance in FindObjectsByType<UI_ISlot>(0))
        {
            instance.UpdateSlot();
        }
    }
}

public enum ISlotType
{
    None,
    Any,
    Acc,
    Out,
    Use
}