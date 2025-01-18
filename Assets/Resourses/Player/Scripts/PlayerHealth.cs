using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100.0f;
    private float _currentHealth;
    [SerializeField] Slider _healthBar;
    public float MaxHealth
    {
        get => _maxHealth;
    }
    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        _healthBar.value = _currentHealth / _maxHealth;
    }

    void Die()
    {
        PlayerRespawn playerRespawn = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerRespawn>();
        playerRespawn.DeathPlayer();
        UpdateHealthBar();
    }
}
