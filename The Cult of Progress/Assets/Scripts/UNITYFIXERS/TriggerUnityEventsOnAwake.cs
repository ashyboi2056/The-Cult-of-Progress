using UnityEngine;
using UnityEngine.Events;

public class TriggerUnityEventsOnAwake : MonoBehaviour
{
    public UnityEvent OnAwake;

    void Awake()
    {
        OnAwake.Invoke();
    }
}