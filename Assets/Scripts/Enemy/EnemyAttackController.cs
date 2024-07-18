using System;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private int _damage;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void Initialize(int damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            var player = other.GetComponent<PlayerController>();
            player.TakeDamage(_damage);
        }
    }
}