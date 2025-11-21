using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PLAYER_Controller : DEBUGMonoBehaviour
{
    public void OnEscape(InputValue inputValue)
    {
        if (UI_Panel.QueryIsAnyPanelOpen()){ UI_Panel.CloseAllPanels(); }
        else {}//Open Menu
    }

    private void UseItemInSlot(int slotIndex)
    {
        var inventory = GetComponent<PLAYER_Inventory>();

        if (slotIndex < 0 || slotIndex >= 10)
        {
            if (debug) Debug.Log($"Invalid slot index: {slotIndex}");
            return;
        }

        var item = inventory.GetInventory()[slotIndex];

        if (item is soDATA_ITEM_Usable usableItem)
        {
            usableItem.Use();

            if (usableItem.STAT_isConsumable){ inventory.TakeItem(slotIndex); }
        }
        else
        {
            if (debug) Debug.Log($"Item in slot {slotIndex + 1} is not usable.");
        }
    }

    public void OnHotkey1(InputValue inputValue) => UseItemInSlot(0);
    public void OnHotkey2(InputValue inputValue) => UseItemInSlot(1);
    public void OnHotkey3(InputValue inputValue) => UseItemInSlot(2);
    public void OnHotkey4(InputValue inputValue) => UseItemInSlot(3);
    public void OnHotkey5(InputValue inputValue) => UseItemInSlot(4);
    public void OnHotkey6(InputValue inputValue) => UseItemInSlot(5);
    public void OnHotkey7(InputValue inputValue) => UseItemInSlot(6);
    public void OnHotkey8(InputValue inputValue) => UseItemInSlot(7);
    public void OnHotkey9(InputValue inputValue) => UseItemInSlot(8);
    public void OnHotkey0(InputValue inputValue) => UseItemInSlot(9);

}