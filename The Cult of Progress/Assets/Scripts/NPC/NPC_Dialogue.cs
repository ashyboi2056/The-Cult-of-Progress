using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class NPC_Dialogue : DEBUGMonoBehaviour
{
    public Flowchart flowchart;

    [SerializeField] private UnityEvent OnFirstGreeting;
    [SerializeField] private UnityEvent OnRecruitmentGENERIC;

    public void TriggerFirstGreeting()
    {
        if (debug){ Debug.Log("FirstGreeting Dialogue Triggered!"); }

        OnFirstGreeting.Invoke();
    }

    public void TriggerRecruitmentGENERIC()
    {
        if (debug){ Debug.Log("RecruitmentGENERIC Dialogue Triggered!"); }

        OnRecruitmentGENERIC.Invoke();
    }
}