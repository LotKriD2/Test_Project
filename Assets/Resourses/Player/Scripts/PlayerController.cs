using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] Joystick _joystick;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _speed = 5f;

    private Vector3 _initialScale;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _initialScale = transform.localScale;
    }
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(_joystick.Horizontal(), _joystick.Vertical());
        _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime);

        if (movement.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        }
        else if (movement.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        }

        if (movement.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            _firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }

        _animator.SetFloat("WalkX", Mathf.Abs(_joystick.Horizontal()));
    }
}
