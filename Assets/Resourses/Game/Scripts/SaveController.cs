using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private GameObject _player;
    private List<GameObject> _enemies;
    private Inventory _inventory;
    private SaveSystem _saveSystem;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
        _saveSystem = GetComponent<SaveSystem>();
    }

    public void Save()
    {
        GameData gameData = _saveSystem.CollectGameData(_player, _inventory);
        _saveSystem.SaveGame(gameData);
    }

    public void Load()
    {
        GameData gameData = _saveSystem.LoadGame();
        _saveSystem.ApplyGameData(gameData, _player, _inventory);
    }
}
