using UnityEngine;
using UnityEngine.Events;

public class UI_RitualPointWindowHandler : DEBUGMonoBehaviour
{
    public MAP_RitualPoint ritualPoint;
    public UnityEvent OnSetup;
    public UnityEvent OnSetdown;

    public void PerformRitual()
    {
        ritualPoint.PerformRitual();
    }

    public void Setup()
    {
        OnSetup.Invoke();
    }

    public void Setdown()
    {
        ritualPoint = null;

        OnSetdown.Invoke();
    }
}