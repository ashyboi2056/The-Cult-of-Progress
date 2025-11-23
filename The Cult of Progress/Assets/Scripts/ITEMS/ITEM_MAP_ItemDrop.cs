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
    }
}