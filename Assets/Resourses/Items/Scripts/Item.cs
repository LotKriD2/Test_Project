using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] string _itemName;
    [SerializeField] Sprite _itemIcon;
    [SerializeField] int _quantity = 1;

    public string ItemName
    {
        get => _itemName;
        set => _itemName = value;
    }
    public Sprite ItemIcon
    {
        get => _itemIcon;
        set => _itemIcon = value; 
    }
    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
    }
}


