using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _offset;

    void Update()
    {
        if (_player != null)
        {
            transform.position = _player.position + _offset;
        }
    }
}
