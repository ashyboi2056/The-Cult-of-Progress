using UnityEngine;

public class UI_TargetUITransparencyOnTargetProximitySPEC : TargetUITransparencyOnTargetProximity
{
    void Awake()
    {
        target = GetComponent<LOCAL_PLAYER_FLAG>().gameObject;
    }
}