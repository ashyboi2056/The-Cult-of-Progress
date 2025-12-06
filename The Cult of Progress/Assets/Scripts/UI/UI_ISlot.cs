using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class UI_ISlot : DEBUGMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Label("Slot ID")] public int slotIndex;
    public Image iconImage;
    public ISlotType slotType;

    private PLAYER_Inventory inventory;
    private Transform originalParent;
    private GameObject placeholder;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        inventory = FindFirstObjectByType<LOCAL_PLAYER_FLAG>()?.GetComponent<PLAYER_Inventory>();
        canvasGroup = iconImage.GetComponent<CanvasGroup>() ?? iconImage.gameObject.AddComponent<CanvasGroup>();
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        var item = inventory?.GetItem(slotIndex);
        iconImage.sprite = item?.STAT_sprite;
        iconImage.enabled = item != null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory == null || inventory.GetItem(slotIndex) == null) return;

        originalParent = iconImage.transform.parent;

        placeholder = new GameObject("Placeholder", typeof(LayoutElement));
        placeholder.transform.SetParent(originalParent);
        var le = placeholder.GetComponent<LayoutElement>();
        le.preferredWidth = iconImage.rectTransform.sizeDelta.x;
        le.preferredHeight = iconImage.rectTransform.sizeDelta.y;

        iconImage.transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        iconImage.rectTransform.localPosition = localPoint;
        */
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(originalParent);
        iconImage.rectTransform.anchoredPosition = Vector2.zero;

        if (placeholder != null) Destroy(placeholder);
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag?.GetComponent<UI_ISlot>();
        if (draggedSlot == null || draggedSlot.slotIndex == slotIndex) return;

        var draggedItem = inventory?.GetItem(draggedSlot.slotIndex);
        if (!QueryCanDrop(draggedItem)) return;

        inventory?.SwapItems(slotIndex, draggedSlot.slotIndex);
        UpdateSlot();
        draggedSlot.UpdateSlot();
    }

    public bool QueryCanDrop(soDATA_ITEM item)
    {
        if (item == null) return false;

        return slotType switch
        {
            ISlotType.Any => true,
            ISlotType.Use => item is soDATA_ITEM_Usable,
            ISlotType.Acc => item is soDATA_ITEM_Accessory,
            _ => false
        };
    }

    public static void UpdateAllUIISlots()
    {
        foreach (var slot in FindObjectsByType<UI_ISlot>(FindObjectsSortMode.None))
        {
            slot.UpdateSlot();
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