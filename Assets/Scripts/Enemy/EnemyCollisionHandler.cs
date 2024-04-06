using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action<float> DamageReceived;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(GlobalConstants.BULLET_TAG))
        {
            var bullet = collision.collider.GetComponent<IBullet>();
            DamageReceived?.Invoke(bullet.Damage);
        }
    }
}