using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private GameObject _player;
    private Inventory _inventory;
    private SaveSystem _saveSystem;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        if (_player == null)
        {
            Debug.LogError("Объект с тегом 'Player' не найден.");
            return;
        }

        _inventory = _player.GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("У игрока отсутствует компонент Inventory.");
            return;
        }

        _saveSystem = GetComponent<SaveSystem>();
        if (_saveSystem == null)
        {
            Debug.LogError("Отсутствует компонент SaveSyste.!");
        }

        Load();
    }

    public void Save()
    {
        if (_saveSystem == null || _player == null || _inventory == null)
        {
            Debug.LogError("Не удалось сохранить игру: отсутствуют необходимые компоненты.");
            return;
        }

        GameData gameData = _saveSystem.CollectGameData(_player, _inventory);
        _saveSystem.SaveGame(gameData);
    }

    public void Load()
    {
        if (_saveSystem == null)
        {
            Debug.LogError("Не удалось загрузить игру: отсутствует компонент SaveSystem.");
            return;
        }

        GameData gameData = _saveSystem.LoadGame();
        if (gameData == null)
        {
            Debug.LogWarning("Файл сохранения не найден или повреждён.");
            return;
        }

        _saveSystem.ApplyGameData(gameData, _player, _inventory);

        UpdatePlayerUI(_player);
    }

    void UpdatePlayerUI(GameObject _player)
    {
        _player.GetComponent<PlayerHealth>().UpdateHealthBar();
        _player.GetComponent<PlayerShoot>().UpdateAmmoUI();
    }
}