using System;
using UnityEngine;

public class AI_ENEMY_Base : DEBUGMonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    private int currentHP;
    [SerializeField] private float moveSpeed = 2f;

    [Space(10)]

    [SerializeField] private Resource resourceRewardType = Resource.None;
    [SerializeField] private int resourceRewardAmount = 0;
    [SerializeField] private EnemyBehaviour behaviour = EnemyBehaviour.Wandering;

    [Space(10)]

    [SerializeField] private soDATA_ITEM[] guaranteedDrops;

    [Space(10)]

    [SerializeField] private GameObject itemDropPrefab;

    private Transform playerTarget;
    private Rigidbody2D rb;
    private Vector2 wanderDirection;
    private float wanderTimer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        currentHP = maxHP;
    }

    private void Update()
    {
        switch (behaviour)
        {
            case EnemyBehaviour.Wandering:
                Wander();
                break;
            case EnemyBehaviour.Chasing:
                ChasePlayer();
                break;
        }
    }

    private void Wander()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
        {
            wanderDirection = UnityEngine.Random.insideUnitCircle.normalized;
            wanderTimer = UnityEngine.Random.Range(2f, 4f); // change direction every 2–4 seconds
        }

        rb.MovePosition(rb.position + wanderDirection * moveSpeed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        
    }

    // Damage from player attacks
    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // Collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitPlayer(collision);
    }

    private void HitPlayer(Collision2D collision)
    {
        var playerHealth = collision.gameObject.GetComponent<PLAYER_Health>();
        if (playerHealth != null)
        {
            // Damage the player's body (or any part you choose)
            playerHealth.Damage(1, PLAYER_Health.GetRandomBodyPart());
        }
    }

    private void Die()
    {
        // Reward player with resources
        DropLoot();

        Destroy(gameObject);
    }

    private void DropLoot()
    {
        FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_Resources>().AddResource(resourceRewardType,resourceRewardAmount);
    
        foreach (soDATA_ITEM item in guaranteedDrops){ DropItem(item); }
    }

    public void DropItem(soDATA_ITEM item)
    {
        // Slight random offset around the drop position
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * 0.1f; // tweak radius as needed
        Vector3 spawnPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        ITEM_MAP_ItemDrop newItemDrop = 
            Instantiate(itemDropPrefab, spawnPos, Quaternion.identity, GameObject.Find("Map").transform)
        .GetComponent<ITEM_MAP_ItemDrop>();

        newItemDrop.Setup(item);
    }
}

public enum EnemyBehaviour
{
    Wandering,
    Chasing
}