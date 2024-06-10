using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    public int Speed => 40;
    public event Action<Bullet> BulletHit; 
    public Rigidbody Rigidbody { get; private set; }
    
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