using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DeathPlayer()
    {
        _player.transform.position = _respawnPoint.transform.position;
        _player.GetComponent<PlayerHealth>().CurrentHealth = _player.GetComponent<PlayerHealth>().MaxHealth;
        _player.GetComponent<PlayerShoot>().ReloadAfterDeath();
        _player.GetComponent<Inventory>().RemoveAllItems();
    }
}
