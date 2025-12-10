using UnityEngine;
using NaughtyAttributes;

public class ITEM_MAP_ItemDrop : DEBUGMonoBehaviour
{
    [OnValueChanged("UpdateUI")]
    [SerializeField] public soDATA_ITEM item;

    [SerializeField] private SpriteRenderer spriteRenderer;

    //Auto Call for Presets [Level Design]
    public void Awake()
    {
        if (item != null) { Setup(item); }
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }

    [Button]
    private void UpdateUI()
    {
        spriteRenderer.sprite = item.STAT_sprite;
    }

    public void Setup(soDATA_ITEM newItem)
    {
        item = newItem;

        UpdateUI();

        // Check for overlap with ItemDropSpots
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("ItemDropSpot"))
            {
                transform.SetParent(hit.transform);
                transform.localPosition = Vector3.zero;

                
                break; // stop after first match
            }
        }

        if (transform.parent.tag == "ItemDropSpot"){ GetComponent<Collider2D>().isTrigger = true; }
        else { GetComponent<Collider2D>().isTrigger = false; }
    }
}