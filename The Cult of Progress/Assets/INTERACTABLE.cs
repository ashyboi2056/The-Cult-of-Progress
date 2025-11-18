using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class INTERACTABLE : DEBUGMonoBehaviour
{
    [SerializeField] private UnityEvent OnInteractEvent;
    [SerializeField] private float interactRange = 3f;

    public void Interact()
    {
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