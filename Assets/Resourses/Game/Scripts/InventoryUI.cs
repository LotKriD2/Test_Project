using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject _itemSlotPrefab;
    [SerializeField] Transform _inventoryPanel;
    [SerializeField] float _offsetSlots = 50.0f;
    private int _countSlots = 0;

    public void UpdateUI(List<Item> items)
    {
        foreach (Transform child in _inventoryPanel)
        {
            Destroy(child.gameObject);
        }
        
        _countSlots = 0;

        float offset = 0.0f;
        foreach (Item item in items)
        {
            GameObject slot = Instantiate(_itemSlotPrefab, _inventoryPanel);

            if(_countSlots > 0)
            {
                offset += _offsetSlots;
                slot.transform.localPosition = new Vector3(slot.transform.localPosition.x - offset, slot.transform.localPosition.y,
                slot.transform.localPosition.z);
            }

            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.ItemIcon;
            slot.transform.Find("ItemCount").GetComponent<Text>().text = item.Quantity.ToString();
            
            Button button = slot.transform.Find("Button").GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => RemoveItemFromInventory(item));
            }

            _countSlots++;
        }
    }
    public void RemoveItemFromInventory(Item item)
    {
        Inventory playerInventory = FindObjectOfType<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.RemoveItem(item);
        }
    }
}