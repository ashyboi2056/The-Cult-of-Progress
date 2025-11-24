//ASH+_GENERIC_SCRIPTS_#6

using UnityEngine;

public class ToggleCursorVisible : MonoBehaviour
{
    public ToggleCursorVisibleMode mode = ToggleCursorVisibleMode.isToggle;
    public bool triggerOnAwake = true;

    void Awake()
    {
        if (triggerOnAwake)
        {
            switch (mode)
            {
                case ToggleCursorVisibleMode.isToggle:
                    Cursor.visible = !Cursor.visible; // Toggles the cursor
                    if (Cursor.lockState == CursorLockMode.None) { Cursor.lockState = CursorLockMode.Locked; } // Locks it to the center of the screen
                    else if ((Cursor.lockState == CursorLockMode.Locked) || (Cursor.lockState == CursorLockMode.Confined)) { Cursor.lockState = CursorLockMode.None; } // Unlocks it to the center of the screen
                    break;
                case ToggleCursorVisibleMode.isMakeInvisible:
                    Cursor.visible = false; // Hides the cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case ToggleCursorVisibleMode.isMakeVisible:
                    Cursor.visible = true; // Unhides the cursor
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
        }
    }
}

public enum ToggleCursorVisibleMode
{
    isToggle,
    isMakeVisible,
    isMakeInvisible
}