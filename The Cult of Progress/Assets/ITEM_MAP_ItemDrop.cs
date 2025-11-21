using UnityEngine;
using NaughtyAttributes;

public class ITEM_MAP_ItemDrop : DEBUGMonoBehaviour
{
    [OnValueChanged("UpdateUI")]
    [SerializeField] public soDATA_ITEM item;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        UpdateUI();
    }

    public void PickUp()
    {
        Destroy(gameObject);
    }

    [Button]
    private void UpdateUI()
    {
        spriteRenderer.sprite = item.STAT_icon;
    }
}