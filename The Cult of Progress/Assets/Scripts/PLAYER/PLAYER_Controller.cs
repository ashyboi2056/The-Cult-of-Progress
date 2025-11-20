using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PLAYER_Controller : DEBUGMonoBehaviour
{
    public void OnEscape(InputValue inputValue)
    {
        if (UI_Panel.QueryIsAnyPanelOpen()){ UI_Panel.CloseAllPanels(); }
        else {}//Open Menu
    }
}