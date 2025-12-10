using UnityEngine;

public class PLAYER_Combat : MonoBehaviour
{
    public float attackRange;
    public int attackDamage;

    [Space(10)]

    public LayerMask targetLayers; // assign "Player" and "Enemy" layers in Inspector

    public void Attack()
    {
        // Find all colliders in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayers);

        foreach (Collider2D hit in hits)
        {
            // Don’t damage yourself
            if (hit.gameObject == gameObject) continue;

            Damage(hit.gameObject, attackDamage);
        }
    }

    private void Damage(GameObject target, int amount)
    {
        if (target.GetComponent<PLAYER_Health>() != null){ DamagePlayer(target.GetComponent<PLAYER_Health>(), amount); }
        else if (target.GetComponent<AI_ENEMY_Base>() != null){ DamageAI(target.GetComponent<AI_ENEMY_Base>(), amount); }
    }

    private void DamagePlayer(PLAYER_Health target, int amount)
    {
        target.Damage(amount, PLAYER_Health.GetRandomBodyPart());
    }
    private void DamageAI(AI_ENEMY_Base target, int amount)
    {
        target.TakeDamage(amount);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}