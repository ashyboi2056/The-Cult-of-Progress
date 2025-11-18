using UnityEngine;
using UnityEngine.Events;

public class INTERACTABLE : DEBUGMonoBehaviour
{
    [SerializeField] private UnityEvent OnInteractEvent;
    [SerializeField] private float interactRange = 3f;

    public void Interact()
    {
        if (debug){ Debug.Log(name + " was Interacted with!"); }

        OnInteractEvent.Invoke();
    }

    public float GetInteractRange()
    {
        return interactRange;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}