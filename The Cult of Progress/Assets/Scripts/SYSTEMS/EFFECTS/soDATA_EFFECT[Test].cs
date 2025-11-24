using UnityEngine;

[CreateAssetMenu(fileName = "New TEST Effect", menuName = "EFFECT/++TEST")]
public class TESTEffect : soDATA_EFFECT
{
    public override void Apply(GameObject target = null)
    {
        Debug.Log("Testing Effect: " + name);
    }
}