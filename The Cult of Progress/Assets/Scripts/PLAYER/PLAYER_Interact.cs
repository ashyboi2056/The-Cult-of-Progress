using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PLAYER_Interact : DEBUGMonoBehaviour
{
    private bool hasInteractedThisPress = false;

    public void OnInteract(InputValue inputValue)
    {
        if (hasInteractedThisPress) return; // Prevent multiple triggers in one press

        INTERACTABLE nearestInteractable = FindNearestInteractable();
        if (nearestInteractable == null){ return; }

        float distance = Vector2.Distance(transform.position, nearestInteractable.transform.position);
        if (distance <= nearestInteractable.GetInteractRange())
        {
            hasInteractedThisPress = true;

            nearestInteractable.Interact();
        }
    }
    
    private INTERACTABLE FindNearestInteractable()
    {
        // Get all interactables in the scene
        INTERACTABLE[] interactables = FindObjectsByType<INTERACTABLE>(FindObjectsSortMode.None);

        if (interactables.Length == 0) return null;

        // Find the closest one
        INTERACTABLE nearest = interactables
            .OrderBy(i => Vector3.Distance(transform.position, i.transform.position))
            .FirstOrDefault();

        return nearest;
    }
}