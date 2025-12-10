using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SocialPlatforms;
using TMPro;

public class PLAYER_Inventory : DEBUGMonoBehaviour
{
    [SerializeField] private soDATA_ITEM[] inventory = new soDATA_ITEM[10];
    [SerializeField] private soDATA_ITEM[] outfitSlot = new soDATA_ITEM[1];
    [SerializeField] private soDATA_ITEM[] accessorySlots = new soDATA_ITEM[3];

    public bool PickUpItem(soDATA_ITEM item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (QuerySlotEmpty(i))
            {
                inventory[i] = item;
                UpdateUI();

                return true;
            }
        }

        return false;
    }

    public void TakeItem(int slotIndex)
    {
        if (debug) { Debug.Log("Taking Item at: " + slotIndex); }

        bool isItemTaken = false;

        switch (slotIndex)
        {
            case 100: //Outfit Slot
                outfitSlot[0] = null;
                isItemTaken = true;
                break;
            case 200: //Acc 1
                accessorySlots[0] = null;
                isItemTaken = true;
                break;
            case 201: //Acc 2
                accessorySlots[1] = null;
                isItemTaken = true;
                break;
            case 202: //Acc 3
                accessorySlots[2] = null;
                isItemTaken = true;
                break;
        }
        if (!isItemTaken == true)
        {
            inventory[slotIndex] = null;
            isItemTaken = true;
        }

        UpdateUI();

        if (isItemTaken == false) { Debug.Log("CRITICAL ERROR: Item Taken Attempt but no Item Taken at: " + name + ". With ID: " + slotIndex); }
    }

    public void SwapItems(int slotIndex1, int slotIndex2)
    {
        soDATA_ITEM item1 = GetItem(slotIndex1);
        soDATA_ITEM item2 = GetItem(slotIndex2);

        if (slotIndex1 < 10) { inventory[slotIndex1] = item2; }
        else
        {
            switch (slotIndex1)
            {
                case 100: //Outfit Slot
                    outfitSlot[0] = item2;
                    break;
                case 200: //Acc 1
                    EquipAcc(item2 as soDATA_ITEM_Accessory, 200);
                    break;
                case 201: //Acc 2
                    EquipAcc(item2 as soDATA_ITEM_Accessory, 201);
                    break;
                case 202: //Acc 3
                    EquipAcc(item2 as soDATA_ITEM_Accessory, 202);
                    break;
            }
        }

        if (slotIndex2 < 10) { inventory[slotIndex2] = item1; }
        else
        {
            switch (slotIndex2)
            {
                case 100: //Outfit Slot
                    outfitSlot[0] = item1;
                    break;
                case 200: //Acc 1
                    EquipAcc(item1 as soDATA_ITEM_Accessory, 200);
                    break;
                case 201: //Acc 2
                    EquipAcc(item1 as soDATA_ITEM_Accessory, 201);
                    break;
                case 202: //Acc 3
                    EquipAcc(item1 as soDATA_ITEM_Accessory, 202);
                    break;
            }
        }
    }

    public bool StoreItem(soDATA_ITEM item, int slotIndex)
    {
        if (QuerySlotEmpty(slotIndex))
        {
            if (slotIndex < 10) { inventory[slotIndex] = item; }
            else
            {
                switch (slotIndex)
                {
                    case 100: //Outfit Slot
                        outfitSlot[0] = item;
                        break;
                    case 200: //Acc 1
                        EquipAcc(item as soDATA_ITEM_Accessory, 200);
                        break;
                    case 201: //Acc 2
                        EquipAcc(item as soDATA_ITEM_Accessory, 201);
                        break;
                    case 202: //Acc 3
                        EquipAcc(item as soDATA_ITEM_Accessory, 202);
                        break;
                }
            }
        }

        return false;
    }

    [Button]
    public void EmptyInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = null;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        UI_ISlot.UpdateAllUIISlots();
    }

    public soDATA_ITEM[] GetInventory()
    {
        return inventory;
    }

    public soDATA_ITEM GetItem(int location)
    {
        if (location < 10) { return inventory[location]; }
        else
        {
            switch (location)
            {
                case 100: //Outfit Slot
                    return outfitSlot[0];
                case 200: //Acc 1
                    return accessorySlots[0];
                case 201: //Acc 2
                    return accessorySlots[1];
                case 202: //Acc 3
                    return accessorySlots[2];
            }
        }
        return null;
    }

    public void SetItem(int location, soDATA_ITEM item) //Use Carefully
    {
        if (location < 10) { inventory[location] = item; }
        else
        {
            switch (location)
            {
                case 100: //Outfit Slot
                   outfitSlot[0] = item;
                   break;
                case 200: //Acc 1
                    accessorySlots[0] = item;
                   break;
                case 201: //Acc 2
                    accessorySlots[1] = item;
                   break;
                case 202: //Acc 3
                    accessorySlots[2] = item;
                   break;
            }
        }
    }

    public bool QuerySlotEmpty(int location)
    {
        if (location < 10)
        {
            if (inventory[location] == null) { return true; }
            else { return false; }
        }
        else
        {
            switch (location)
            {
                case 100: //Outfit Slot
                    if (outfitSlot[0] == null) { return true; }
                    return false;
                case 200: //Acc 1
                    if (accessorySlots[0] == null) { return true; }
                    return false;
                case 201: //Acc 2
                    if (accessorySlots[1] == null) { return true; }
                    return false;
                case 202: //Acc 3
                    if (accessorySlots[2] == null) { return true; }
                    return false;
            }
        }

        return false;
    }

    public int LocationIDQueryCanEquipAcc()
    {
        if (accessorySlots[0] == null) { return 200; }
        if (accessorySlots[1] == null) { return 201; }
        if (accessorySlots[2] == null) { return 202; }

        return -1;
    }

    public void EquipAcc(soDATA_ITEM_Accessory accItem, int location)
    {
        if (accItem == null) { TakeItem(location); }
        else if (QuerySlotEmpty(location))
        {
            switch (location)
            {
                case 200: //Acc 1
                    accessorySlots[0] = accItem;
                    ApplyACCItemEquipEffects(accItem);
                    break;
                case 201: //Acc 2
                    accessorySlots[1] = accItem;
                    ApplyACCItemEquipEffects(accItem);
                    break;
                case 202: //Acc 3
                    accessorySlots[2] = accItem;
                    ApplyACCItemEquipEffects(accItem);
                    break;
            }

            UpdateUI();
        }
        else { Debug.Log("CRITICAL ERROR: Equipped Item Overrode occupied Slot!"); }
    }

    private void ApplyACCItemEquipEffects(soDATA_ITEM_Accessory accItem)
    {
        foreach (soDATA_EFFECT effect in accItem.STAT_OnEquipEFFECTS) { effect.Apply(); }
    }
}