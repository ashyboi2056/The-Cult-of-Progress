//ASH+_GENERIC_SCRIPTS_#3

using UnityEngine;

public class DDoL : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}