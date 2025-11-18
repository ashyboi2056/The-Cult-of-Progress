using NaughtyAttributes;
using UnityEngine;

public class DEBUGMonoBehaviour : MonoBehaviour
{
    //Debug Debugging!
    [OnValueChanged("DEBUGDisableThis")]
    public bool ShowDebugOptions;

    [ShowIf("ShowDebugOptions")]
    [Button("Disable All Debugging!")]
    private void DEBUGDisableAll()
    {
        foreach (DEBUGMonoBehaviour debugScript in FindObjectsByType<DEBUGMonoBehaviour>(FindObjectsSortMode.None))
        {
            debugScript.DEBUGDisableThis(); // Example: disable debug on all

            debugScript.ShowDebugOptions = false;
        }
    }

    [ShowIf("ShowDebugOptions")]
    [Button("Enable All Debugging!")]
    private void DEBUGEnableAll()
    {
        foreach (DEBUGMonoBehaviour debugScript in FindObjectsByType<DEBUGMonoBehaviour>(FindObjectsSortMode.None))
        {
            debugScript.DEBUGEnableThis(); // Example: disable debug on all
        }
    }

    [ShowIf("ShowDebugOptions")]
    [SerializeField]
    public bool debug;

    private void DEBUGDisableThis()
    {
        debug = false;
    }

    private void DEBUGEnableThis()
    {
        debug = true;
    }
}