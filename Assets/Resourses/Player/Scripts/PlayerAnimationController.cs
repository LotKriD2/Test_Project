using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GetComponent<PlayerController>().Movement.x == 0 && 
        GetComponent<PlayerController>().Movement.y == 0)
        {
            _animator.SetBool("IsWalk", false);
        }
        else
        {
            _animator.SetBool("IsWalk", true);
        }
    }
}
