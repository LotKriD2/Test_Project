using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] int _maxCountEnemy = 3;
    [SerializeField] float _minSpawnRange = -20.0f;
    [SerializeField] float _maxSpawnRange = 20.0f;
    private int _currentCountEnemy = 0;
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        while (_currentCountEnemy < _maxCountEnemy)
        {
            float x = Random.Range(_minSpawnRange, _maxSpawnRange);
            float y = Random.Range(_minSpawnRange, _maxSpawnRange);
            Vector2 spawnPosition = new Vector2(x, y);

            if (!Physics2D.OverlapCircle(spawnPosition, 0.5f)) 
            {
                Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
                _currentCountEnemy++;
            }
        }
    }

    public void DeathEnemy()
    {
        _currentCountEnemy--;
        
        if(_currentCountEnemy == 0)
        {
            SpawnEnemies();
        }
    }
}
