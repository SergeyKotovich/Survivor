using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet> BulletHit; 
    public Rigidbody Rigidbody { get; private set; }
    public int Speed { get; private set; } = 250;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody.useGravity = true;
        BulletHit?.Invoke(this);
        
    }
}