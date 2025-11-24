using UnityEngine;
using System.Collections.Generic;

public class PLAYER_NPC_RecruitedNPCs : DEBUGMonoBehaviour
{
    public List<NPC_IDEnums> recruitedNPCs;

    public void AddRecruitedNPC(NPC_IDEnums recruitedNPC)
    {
        recruitedNPCs.Add(recruitedNPC);
    }
}