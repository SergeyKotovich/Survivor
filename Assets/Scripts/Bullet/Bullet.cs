using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public event Action<Bullet> BulletHit; 
    public Rigidbody Rigidbody { get; private set; }
    public int Speed => 50;
    public float Damage { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        Rigidbody.useGravity = true;
        BulletHit?.Invoke(this);
    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }
}