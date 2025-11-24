//ASH+_GENERIC_SCRIPTS_#5

using UnityEngine;

public class SwitchTargetsActiveState : MonoBehaviour
{
    public GameObject target;

    public void ToggleActive(){
        target.SetActive(!target.activeInHierarchy);
    }
}