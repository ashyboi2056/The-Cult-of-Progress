using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Effect", menuName = "EFFECT/++Heal")]
public class HealEffect : soDATA_EFFECT
{
    [SerializeField] private int amount;
    [SerializeField] private BodyPart targetBodyPart;

    public override void Apply(GameObject target = null)
    {
        // Bad: Relys on only Local Player having PLAYER_Health
        FindFirstObjectByType<LOCAL_PLAYER_FLAG>().gameObject.GetComponent<PLAYER_Health>().Heal(amount, targetBodyPart);
    }
}