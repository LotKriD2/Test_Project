using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectibleItem : MonoBehaviour
{
    [SerializeField] string _itemName;
    [SerializeField]Sprite _itemIcon;
    [SerializeField] int _quantity = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory playerInventory = collision.GetComponent<Inventory>();

            if (playerInventory != null)
            {
                Item newItem = new Item
                {
                    ItemName = _itemName,
                    ItemIcon = _itemIcon,
                    Quantity = _quantity
                };

                playerInventory.AddItem(newItem);
                Destroy(gameObject); 
            }
        }
    }
}
