//ASH+_GENERIC_SCRIPTS_#7

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKeyInvokeEvent : MonoBehaviour
{
    public List<KeyCode> keybinds;

    public UnityEvent effect;

    void Update()
    {
        foreach (KeyCode keybind in keybinds)
        {
            if (Input.GetKeyDown(keybind))
            {
                effect.Invoke();
            }
        }
    }
}