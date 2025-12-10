using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using System.Collections.Generic;

public class UI_SSlot : DEBUGMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Label("Slot Type")] public ISlotType slotType;
    public Image iconImage;

    [Space(10)]
    private Transform originalParent;
    private GameObject placeholder;
    private CanvasGroup canvasGroup;

    [Space(10)]
    [SerializeField] private GameObject itemDropPrefab;

    // Self-contained item container
    [SerializeField] private soDATA_ITEM containedItem;

    void Awake()
    {
        canvasGroup = iconImage.GetComponent<CanvasGroup>() ?? iconImage.gameObject.AddComponent<CanvasGroup>();
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        iconImage.sprite = containedItem?.STAT_sprite;
        iconImage.enabled = containedItem != null;
    }

    public void SetItem(soDATA_ITEM newItem)
    {
        containedItem = newItem;
        UpdateSlot();
    }

    public soDATA_ITEM GetItem()
    {
        return containedItem;
    }

    public void ClearItem()
    {
        containedItem = null;
        UpdateSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (containedItem == null) return;

        originalParent = iconImage.transform.parent;

        placeholder = new GameObject("Placeholder", typeof(LayoutElement));
        placeholder.transform.SetParent(originalParent);
        var le = placeholder.GetComponent<LayoutElement>();
        le.preferredWidth = iconImage.rectTransform.sizeDelta.x;
        le.preferredHeight = iconImage.rectTransform.sizeDelta.y;

        iconImage.transform.SetParent(transform.root.gameObject.GetComponentInChildren<Canvas>().transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 TARGET = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        iconImage.transform.position = TARGET;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconImage.transform.SetParent(originalParent);
        iconImage.rectTransform.anchoredPosition = Vector2.zero;

        if (placeholder != null) Destroy(placeholder);
        canvasGroup.blocksRaycasts = true;

        // If not dropped on another slot, drop to floor
        if (!QuerySlotBelow(Input.mousePosition))
        {
            FloorDrop(containedItem);
            ClearItem();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Try to get either type of slot
        var draggedISlot = eventData.pointerDrag?.GetComponent<UI_ISlot>();
        var draggedSSlot = eventData.pointerDrag?.GetComponent<UI_SSlot>();

        // Case 1: dragged from ISlot (inventory slot)
        if (draggedISlot != null)
        {
            var draggedItem = draggedISlot.inventory?.GetItem(draggedISlot.slotIndex);
            if (!QueryCanDrop(draggedItem)) return;

            // Swap: put dragged item here, send this slot’s item back to inventory
            var temp = containedItem;
            SetItem(draggedItem);
            draggedISlot.inventory?.SetItem(draggedISlot.slotIndex, temp);
            draggedISlot.UpdateSlot();
            return;
        }

        // Case 2: dragged from another SSlot
        if (draggedSSlot != null && draggedSSlot != this)
        {
            var draggedItem = draggedSSlot.GetItem();
            if (!QueryCanDrop(draggedItem)) return;

            var temp = containedItem;
            SetItem(draggedItem);
            draggedSSlot.SetItem(temp);
            return;
        }
    }

    void FloorDrop(soDATA_ITEM item)
    {
        if (item == null) return;

        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * 0.1f;
        Vector2 spawnPos = newPos + randomOffset;

        ITEM_MAP_ItemDrop newItemDrop =
            Instantiate(itemDropPrefab, spawnPos, Quaternion.identity, GameObject.Find("Map").transform)
            .GetComponent<ITEM_MAP_ItemDrop>();

        newItemDrop.Setup(item);
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

    public bool QuerySlotBelow(Vector3 screenPos)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (debug) { Debug.Log("UI Hit: " + result.gameObject.name); }

            if (result.gameObject.GetComponent<UI_SSlot>())
            {
                return true;
            }
        }

        return false;
    }

    public static void UpdateAllUISSlots()
    {
        foreach (var slot in FindObjectsByType<UI_SSlot>(FindObjectsSortMode.None))
        {
            slot.UpdateSlot();
        }
    }
}