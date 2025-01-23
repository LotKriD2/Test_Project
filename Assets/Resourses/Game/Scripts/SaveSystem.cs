using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string _savePath;

    public void SaveGame(GameData gameData)
    {
        _savePath = Application.persistentDataPath + "/save.json";
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(_savePath, json);
    }
    public GameData LoadGame()
    {
        _savePath = Application.persistentDataPath + "/save.json";
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            return gameData;
        }
        else
        {
            return null;
        }
    }

    public GameData CollectGameData(GameObject player, Inventory inventory)
    {
        GameData data = new GameData
        {
            playerX = player.transform.position.x,
            playerY = player.transform.position.y,
            playerHealth = player.GetComponent<PlayerHealth>().CurrentHealth,
            ammoCount = player.GetComponent<PlayerShoot>().CurrentAmmo,
            inventoryItems = new List<InventoryItemData>(),
            enemies = new List<EnemyData>()
        };

        foreach (var item in inventory.Items)
        {
            data.inventoryItems.Add(new InventoryItemData
            {
                itemName = item.ItemName,
                quantity = item.Quantity
            });
        }

        return data;
    }

    public void ApplyGameData(GameData data, GameObject player, Inventory inventory)
    {
        player.transform.position = new Vector3(data.playerX, data.playerY, 0);
        player.GetComponent<PlayerHealth>().CurrentHealth = data.playerHealth;
        player.GetComponent<PlayerShoot>().CurrentAmmo = data.ammoCount;

        inventory.Items.Clear();
        foreach (var itemData in data.inventoryItems)
        {
            Item item = new Item
            {
                ItemName = itemData.itemName,
                Quantity = itemData.quantity
            };
            inventory.AddItem(item);
        }
    }
}
