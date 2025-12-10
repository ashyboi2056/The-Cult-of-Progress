using UnityEngine;
using System.Collections.Generic;

public class MAP_CommenceRitualSpot : DEBUGMonoBehaviour
{
    public GameObject focalItemSpot;
    public GameObject[] focalBoundSpots = new GameObject[2];

    [Space(10)]
    public GameObject[] reqSpots = new GameObject[4];

    private soDATA_RITUAL activeRitual;

    // Cached state at ritual start
    private NPC_IDEnums[] initialBound;
    private soDATA_ITEM[] initialItems;

    void Update()
    {
        if (activeRitual != null && AnyBoundOrItemChanged())
        {
            UI_DATA_RitualManager.COMMANDInterruptRitual(activeRitual);
            activeRitual = null; // clear after interrupt
        }
    }

    public void AttemptCommenceRitual()
    {
        NPC_IDEnums[] bound = GatherBound();
        soDATA_ITEM[] items = GatherItems();

        var playerData = FindFirstObjectByType<LOCAL_PLAYER_FLAG>()
            .GetComponent<PLAYER_CharacterData>();

        foreach (soDATA_RITUAL ritual in playerData.chosenCult.STAT_victoryRituals)
        {
            if (ritual == null)
            {
                Debug.LogError("CRITICAL ERROR: No Victory Ritual at: " + playerData.chosenCult);
                return;
            }

            if (UI_DATA_RitualManager.QueryStartRitual(ritual, bound, items))
            {
                UI_DATA_RitualManager.COMMANDStartRitual(ritual);
                activeRitual = ritual;

                // Cache the state at ritual start
                initialBound = bound;
                initialItems = items;
            }
        }
    }

    NPC_IDEnums[] GatherBound()
    {
        List<NPC_IDEnums> bound = new List<NPC_IDEnums>();

        void CollectFrom(IEnumerable<GameObject> containers)
        {
            foreach (GameObject container in containers)
            {
                var npc = container.GetComponent<NPC_MAP_Container>();
                if (npc == null) continue; // safety check
                if (npc.bound == NPC_IDEnums.None) continue;

                bound.Add(npc.bound);
            }
        }

        CollectFrom(focalBoundSpots);
        CollectFrom(reqSpots);

        return bound.ToArray();
    }

    soDATA_ITEM[] GatherItems()
    {
        List<soDATA_ITEM> items = new List<soDATA_ITEM>();

        void CollectFrom(IEnumerable<GameObject> containers)
        {
            foreach (GameObject container in containers)
            {
                ITEM_MAP_ItemDrop itemContainer = container.GetComponentInChildren<ITEM_MAP_ItemDrop>();
                if (itemContainer == null) continue; // safety check
                if (itemContainer.item == null) continue; // skip empty slots

                items.Add(itemContainer.item);
            }
        }

        // Collect from focal item spot (single GameObject)
        if (focalItemSpot != null)
        {
            ITEM_MAP_ItemDrop itemContainer = focalItemSpot.GetComponentInChildren<ITEM_MAP_ItemDrop>();
            if (itemContainer != null && itemContainer.item != null)
            {
                items.Add(itemContainer.item);
            }
        }

        // Collect from bound and required spots
        CollectFrom(focalBoundSpots);
        CollectFrom(reqSpots);

        return items.ToArray();
    }

    private bool AnyBoundOrItemChanged()
    {
        var currentBound = GatherBound();
        var currentItems = GatherItems();

        // Compare bound NPCs (length or content mismatch)
        if (currentBound.Length != initialBound.Length) return true;
        for (int i = 0; i < currentBound.Length; i++)
        {
            if (currentBound[i] != initialBound[i]) return true;
        }

        // Compare items (length or content mismatch)
        if (currentItems.Length != initialItems.Length) return true;
        for (int i = 0; i < currentItems.Length; i++)
        {
            if (currentItems[i] != initialItems[i]) return true;
        }

        return false;
    }
}