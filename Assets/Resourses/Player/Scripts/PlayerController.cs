using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] Joystick _joystick;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _speed = 5f;

    private Vector3 _initialScale;
    private Vector2 _movement;
    public Vector2 Movement => _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialScale = transform.localScale;
    }
    void FixedUpdate()
    {
        _movement = new Vector2(_joystick.Horizontal(), _joystick.Vertical());
        _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);

        if (_movement.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        }
        else if (_movement.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        }

        if (_movement.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;
            _firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
