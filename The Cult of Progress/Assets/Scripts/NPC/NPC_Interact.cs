using UnityEngine;
using NaughtyAttributes;

public class NPC_Interact : DEBUGMonoBehaviour
{
    [SerializeField] private NPC_Dialogue dialogue;
    [SerializeField] private NPC_LOCALRecruitmentHandler recruiter;
    //Actual Data visible in Inspector
    [SerializeField][Label("Runtime Data")] private NPC_RuntimeData DATA_data;

    //Public Facing Var
    [HideInInspector] public NPC_RuntimeData data
    {
        get
        {
            return DATA_data;
        }
        private set
        {
            DATA_data = value;
        }
    }

    public void OnInteract()
    {
        recruiter.OnInteract();

        if (!data.hasBeenMet)
        {
            dialogue.TriggerFirstGreeting();

            data.hasBeenMet = true;
        }
    }
}