using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _detectionRadius = 5.0f;

    [SerializeField] float _attackRange = 1.0f;
    [SerializeField] float _attackCooldown = 1.0f;

    private bool _isPlayerInRange = false;
    private bool _isAttacking = false;

    [SerializeField] float _attackDamage = 20.0f;
    private float _nextAttackTime = 0f;
    
    public bool IsPlayerInRange => _isPlayerInRange;
    public bool IsAttacking => _isAttacking;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
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
        Vector2 movement = (_player.position - transform.position).normalized;
        transform.Translate(movement * _speed * Time.deltaTime, Space.World);

        if (movement.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void AttackPlayer()
    {
        if (_isAttacking) return;

        _nextAttackTime = Time.time + _attackCooldown;

        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_attackDamage);
        }
    }
}
