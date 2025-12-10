using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using NaughtyAttributes;

public class PLAYER_Interact : DEBUGMonoBehaviour
{
    [OnValueChanged("Setup")]
    public InputActionAsset inputActionAsset;
    private InputAction interactAction;

    private bool hasInteractedThisPress = false;

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
        InputActionMap inputActionMap = inputActionAsset.FindActionMap("Player");
        interactAction = inputActionMap.FindAction("Interact");
    }

    public void OnInteract(InputValue inputValue)
    {
        if (hasInteractedThisPress) return; // Prevent multiple triggers in one press

        INTERACTABLE nearestInteractable = FindNearestInteractable();
        if (nearestInteractable == null){ return; }

        if (debug){ Debug.Log(nearestInteractable); }

        float distance = Vector2.Distance(transform.position, nearestInteractable.transform.position);
        if (debug){ Debug.Log("Is at distance: " + distance); }
        if (distance <= nearestInteractable.GetInteractRange())
        {
            hasInteractedThisPress = true;

            if (nearestInteractable.GetComponent<ITEM_MAP_ItemDrop>() != null)
            {
                ITEM_MAP_ItemDrop itemOnMap = nearestInteractable.GetComponent<ITEM_MAP_ItemDrop>();
                if (GetComponent<PLAYER_Inventory>().PickUpItem(itemOnMap.item)){ nearestInteractable.Interact(); }
            }
            else
            {
                nearestInteractable.Interact();
            }
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

    private void OnInteractKeyRelease(InputAction.CallbackContext context)
    {
        hasInteractedThisPress = false;
    }
    
    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.canceled += OnInteractKeyRelease; // Triggered when key is lifted
    }

    private void OnDisable()
    {
        interactAction.canceled -= OnInteractKeyRelease;
        interactAction.Disable();
    }

}