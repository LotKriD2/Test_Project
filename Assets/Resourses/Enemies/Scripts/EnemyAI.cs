using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] GameController _gameController;
    [SerializeField] Transform _player;
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _detectionRadius = 5.0f;
    [SerializeField] float _attackRange = 1.0f;
    [SerializeField] float _attackCooldown = 1.0f;

    private bool _isPlayerInRange = false;
    private bool _isAttacking = false;
    [SerializeField] float _maxHealth = 100.0f;
    private float _currentHealth;
    [SerializeField] Slider _healthBar;

    [SerializeField] float _attackDamage = 20.0f;
    private float _nextAttackTime = 0f;
    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _detectionRadius)
        {
            _isPlayerInRange = true;
        }
        else
        {
            _isPlayerInRange = false;
        }

        if (_isPlayerInRange && distanceToPlayer > _attackRange)
        {
            MoveTowardsPlayer();
        }

        if (_isPlayerInRange && distanceToPlayer <= _attackRange && Time.time >= _nextAttackTime)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        _animator.SetFloat("WalkX", Mathf.Abs(direction.x));

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
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
        _gameController.DeathEnemy();
        Destroy(gameObject);
    }

    void AttackPlayer()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        _animator.SetBool("Attack", true);
        _nextAttackTime = Time.time + _attackCooldown;

        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_attackDamage);
        }

        _isAttacking = false;
        _animator.SetBool("Attack", false);
    }
}
