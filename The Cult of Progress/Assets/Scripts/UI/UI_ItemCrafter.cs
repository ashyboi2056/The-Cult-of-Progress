using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class UI_ItemCrafter : DEBUGMonoBehaviour
{
    [SerializeField][Expandable] private soDATA_ITEM item;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject itemDropPrefab;

    public void SetItem(soDATA_ITEM newItem)
    {
        item = newItem;

        Setup();
    }

    private void Setup()
    {
        if (debug) { Debug.Log("Running Setup at: " + name); }

        icon.sprite = item.STAT_sprite;
    }

    public void CraftItem()
    {
        if (!QueryCanCraft()) { return; }
        FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_Resources>().TakeResources(item.STAT_craftingReq);

        Vector3 playerPos = FindFirstObjectByType<LOCAL_PLAYER_FLAG>().transform.position;
        Vector3 playerFacing = FindFirstObjectByType<LOCAL_PLAYER_FLAG>().transform.up;
        Vector3 spawnPos = playerPos + (0.5f * playerFacing);

        ITEM_MAP_ItemDrop newItemDrop = Instantiate(itemDropPrefab,spawnPos,Quaternion.identity,GameObject.Find("Map").transform).GetComponent<ITEM_MAP_ItemDrop>();

        newItemDrop.Setup(item);
    }

    private bool QueryCanCraft()
    {
        if (FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_Resources>().QueryHasResources(item.STAT_craftingReq)) { return true; }

        return false;
    }
}