using System;
using UnityEngine;

public class EnemyAnimationController: MonoBehaviour
{
    private Animator _animator;
    private static readonly int _attack = Animator.StringToHash("attack");
    private static readonly int _walking = Animator.StringToHash("walking");
    private static readonly int _death = Animator.StringToHash("death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowAttack()
    {
       _animator.SetTrigger(_attack);
    }

    public void ShowWalking()
    {
        _animator.SetTrigger(_walking);
    }

    public void ShowDeath()
    {
        _animator.SetTrigger(_death);
    }
    
}