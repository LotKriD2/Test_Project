using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string _savePath;

    private void Start()
    {
        _savePath = Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(_savePath, json);
        Debug.Log("Игра сохранена в " + _savePath);
    }
    public GameData LoadGame()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Игра загружена из " + _savePath);
            return gameData;
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден!");
            return null;
        }
    }
    public GameData CollectGameData(GameObject player, Inventory inventory, List<GameObject> enemies)
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

        foreach (var enemy in enemies)
        {
            data.enemies.Add(new EnemyData
            {
                enemyX = enemy.transform.position.x,
                enemyY = enemy.transform.position.y,
                health = enemy.GetComponent<EnemyAI>().CurrentHealth
            });
        }

        return data;
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

    public void ApplyGameData(GameData data, GameObject player, Inventory inventory, List<GameObject> enemies)
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

        for (int i = 0; i < data.enemies.Count && i < enemies.Count; i++)
        {
            enemies[i].transform.position = new Vector3(data.enemies[i].enemyX, data.enemies[i].enemyY, 0);
            enemies[i].GetComponent<EnemyAI>().CurrentHealth = data.enemies[i].health;
        }
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
