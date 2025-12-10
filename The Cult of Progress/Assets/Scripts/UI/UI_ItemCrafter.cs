using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class UI_ItemCrafter : DEBUGMonoBehaviour
{
    [SerializeField][Expandable] private soDATA_ITEM item;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject itemDropPrefab;

    [Space(10)]
    
    [SerializeField] private TextMeshProUGUI[] counters = new TextMeshProUGUI[5]; 

    public void SetItem(soDATA_ITEM newItem)
    {
        item = newItem;

        Setup();
    }

    private void Setup()
    {
        if (debug) { Debug.Log("Running Setup at: " + name); }

        icon.sprite = item.STAT_sprite;

        counters[0].text = "Food: " + item.STAT_craftingReq[Resource.Food].ToString();
        counters[1].text = "Books: " + item.STAT_craftingReq[Resource.Books].ToString();
        counters[2].text = "Metal: " + item.STAT_craftingReq[Resource.Metal].ToString();
        counters[3].text = "Gold: " + item.STAT_craftingReq[Resource.Gold].ToString();
        counters[4].text = "Souls: " + item.STAT_craftingReq[Resource.Souls].ToString();
    }

    public void CraftItem()
    {
        if (!QueryCanCraft()) { return; }
        FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_Resources>().TakeResources(item.STAT_craftingReq);

        DropItem();
    }

    public void DropItem()
    {
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