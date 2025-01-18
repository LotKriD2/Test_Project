using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> _items = new List<Item>();
    [SerializeField] InventoryUI _inventoryUI;
    [SerializeField] int _maxSlots = 3;
    [SerializeField] SaveController _saveController;
    public List<Item> Items => _items;

    public bool AddItem(Item newItem)
    {
        if (_items.Count >= _maxSlots)
        {
            return false;
        }

        Item existingItem = _items.Find(item => item.ItemName == newItem.ItemName);

        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
        }
        else
        {
            _items.Add(newItem);
        }

        _inventoryUI.UpdateUI(_items);

        _saveController.Save();

        return true;
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (_items.Contains(itemToRemove))
        {
            _items.Remove(itemToRemove);
            _inventoryUI.UpdateUI(_items);
            _saveController.Save();
        }
    }
    public void RemoveAllItems()
    {
        _items = new List<Item>();
        _inventoryUI.UpdateUI(_items);
        _saveController.Save();
    }
}
