using System;
using UnityEngine;

public class EnemyAnimationController: MonoBehaviour
{
    private Animator _animator;
    
    private static readonly int _walking = Animator.StringToHash("walking");
    private static readonly int _death = Animator.StringToHash("death");
    private static readonly int _idle = Animator.StringToHash("idle");
    private static readonly int _attack = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowAttack()
    {
       _animator.SetBool(_attack, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(_attack, false);
    }

    public void ShowWalking()
    {
        _animator.SetTrigger(_walking);
    }

    public void ShowDeath()
    {
        _animator.SetTrigger(_death);
    }

    public void ShowIdle()
    {
        _animator.SetTrigger(_idle);
    }
}