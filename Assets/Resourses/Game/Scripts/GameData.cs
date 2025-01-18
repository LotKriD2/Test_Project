using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public float playerX;
    public float playerY;
    public float playerHealth;
    public int ammoCount;
    public List<InventoryItemData> inventoryItems;
    public List<EnemyData> enemies;
}

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public int quantity;
}

[System.Serializable]
public class EnemyData
{
    public float enemyX;
    public float enemyY;
    public float health;
}