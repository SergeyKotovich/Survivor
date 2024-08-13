using System;
using UnityEngine;
using VContainer;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private BulletsPool _bulletsPool;
    private float _damage;
    private PlayerConfig _playerConfig;

    public void Initialize(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _damage = _playerConfig.Damage;
    }
    
    public void Shoot()
    {
        var bullet = _bulletsPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.Rigidbody.useGravity = false;
        bullet.transform.SetParent(null);
        bullet.SetDamage(_damage);
        bullet.Rigidbody.velocity = (transform.forward * bullet.Speed);
    }
    
}
