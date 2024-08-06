using System;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private int _damage;
    
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
            SoundsManager.Instance.PlayZombieAttack();
        }
    }
}