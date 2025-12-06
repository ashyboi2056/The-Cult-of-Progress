using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_NETWORK_Setup : NetworkBehaviour
{
    public MonoBehaviour[] localOnlyScripts;
    public GameObject[] localOnlyObjects;

    // Awake runs before ownership is assigned
    public override void OnNetworkSpawn()
    {
        
        if (IsOwner){ return; }

        foreach (MonoBehaviour monoBehaviour in localOnlyScripts){ monoBehaviour.enabled = false; }

        foreach (GameObject gObject in localOnlyObjects){ gObject.SetActive(false); }
    }
}