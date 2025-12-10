using System.Linq;
using UnityEngine;
/*
public class MAP_RitualPoint : DEBUGMonoBehaviour
{
    public soDATA_RITUAL ritual;

    [SerializeField] private UI_RitualPointWindowHandler ritualUIManager;

    public void Setup()
    {
        ritualUIManager.ritualPoint = this;

        ritualUIManager.Setup();
    }

    public void PerformRitual()
    {
        if (debug){ Debug.Log("Attempting to perform Ritual at: " + gameObject.name); }

        if (!QueryCanPerformRitual())
        {
            if (debug){ Debug.Log("Ritual Failed to Perform at: " + gameObject.name); }
            return;
        }

        ritual.Performed();
    }

    private bool QueryCanPerformRitual()
    {
        GameObject localPlayer = FindFirstObjectByType<LOCAL_PLAYER_FLAG>().gameObject;

        foreach (NPC_IDEnums npc in ritual.STAT_reqNPCs)
        {
            if (!localPlayer.GetComponent<PLAYER_NPC_RecruitedNPCs>().recruitedNPCs.Contains(npc))
            {
                if (debug){ Debug.Log("Missing: " + npc); }
                return false;
            }
        }
        foreach (soDATA_ITEM item in ritual.STAT_reqItems)
        {
            if (!localPlayer.GetComponent<PLAYER_Inventory>().GetInventory().Contains(item))
            {
                if (debug){ Debug.Log("Missing: " + item.STAT_itemName); }
                return false;
            }
        }

        return true;
    }
}
*/