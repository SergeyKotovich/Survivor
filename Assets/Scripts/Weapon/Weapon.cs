using System;
using UnityEngine;
using VContainer;

public class Weapon : MonoBehaviour, IWeapon, IDamageImprovable
{
    public event Action<float, float> DamageUpdated;
    [SerializeField] private BulletsPool _bulletsPool;
    private float _damage;
    private PlayerConfig _playerConfig;

    public void Initialize(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _damage = _playerConfig.Damage;
    }

    private void Start()
    {
        DamageUpdated?.Invoke(_damage, _damage + _playerConfig.DamageImprovementStep);
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

    public void ImproveDamage()
    {
        _damage += _playerConfig.DamageImprovementStep;
        DamageUpdated?.Invoke(_damage, _damage + _playerConfig.DamageImprovementStep);
    }
}
