using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using System.Collections.Generic;

public class UI_ISlot : DEBUGMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Label("Slot ID")] public int slotIndex;
    public Image iconImage;
    public ISlotType slotType;

    [Space(10)]

    public PLAYER_Inventory inventory;
    private Transform originalParent;
    private GameObject placeholder;
    private CanvasGroup canvasGroup;

    [Space(10)]

    [SerializeField] private GameObject itemDropPrefab;
    void Awake()
    {
        inventory = FindFirstObjectByType<LOCAL_PLAYER_FLAG>()?.GetComponent<PLAYER_Inventory>();
        canvasGroup = iconImage.GetComponent<CanvasGroup>() ?? iconImage.gameObject.AddComponent<CanvasGroup>();
        UpdateSlot();
    }

    void Update()
    {
        //Debug.Log(QuerySlotBelow(Input.mousePosition));
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

        //Check for presence of Slot
        if (!QuerySlotBelow(Input.mousePosition))
        {//If no Slot then
            FloorDrop(inventory.GetItem(slotIndex));
            inventory.TakeItem(slotIndex);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedISlot = eventData.pointerDrag?.GetComponent<UI_ISlot>();
        var draggedSSlot = eventData.pointerDrag?.GetComponent<UI_SSlot>();

        // Case 1: dragged from another ISlot
        if (draggedISlot != null && draggedISlot.slotIndex != slotIndex)
        {
            var draggedItem = inventory?.GetItem(draggedISlot.slotIndex);
            if (!QueryCanDrop(draggedItem)) return;

            inventory?.SwapItems(slotIndex, draggedISlot.slotIndex);
            UpdateSlot();
            draggedISlot.UpdateSlot();
            return;
        }

        // Case 2: dragged from an SSlot
        if (draggedSSlot != null)
        {
            var draggedItem = draggedSSlot.GetItem();
            if (!QueryCanDrop(draggedItem)) return;

            // Swap: put dragged item into inventory, send this slot’s item back to the SSlot
            var temp = inventory?.GetItem(slotIndex);
            inventory?.SetItem(slotIndex, draggedItem);
            draggedSSlot.SetItem(temp);

            UpdateSlot();
            draggedSSlot.UpdateSlot();
            return;
        }
    }

    void FloorDrop(soDATA_ITEM item)
    {
        //Create floor object given pos of
        Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Slight random offset around the drop position
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * 0.1f; // tweak radius as needed
        Vector2 spawnPos = newPos + new Vector2(randomOffset.x, randomOffset.y);

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
        // Create a pointer event at the cursor position
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPos
        };

        // Raycast into the UI system
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (debug){ Debug.Log("UI Hit: " + result.gameObject.name); }

            if (result.gameObject.GetComponent<UI_ISlot>()){ return true; }
            if (result.gameObject.GetComponent<UI_SSlot>()){ return true; }
        }

        return false;
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