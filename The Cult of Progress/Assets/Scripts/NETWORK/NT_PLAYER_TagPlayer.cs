using UnityEngine;
using Unity.Netcode;

public class NT_PLAYER_TagPlayer : NetworkBehaviour
{
    public NetworkVariable<bool> isIt = new NetworkVariable<bool>(false);

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return; // server controls game state

        var other = collision.gameObject.GetComponent<NT_PLAYER_TagPlayer>();
        if (isIt.Value && other != null)
        {
            isIt.Value = false;
            other.isIt.Value = true;
        }
    }
}
