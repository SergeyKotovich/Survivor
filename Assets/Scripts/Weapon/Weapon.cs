using UnityEngine;
using VContainer;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private BulletsPool _bulletsPool;
    private float _damage;

    public void Shoot()
    {
        var bullet = _bulletsPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.Rigidbody.useGravity = false;
        bullet.transform.SetParent(null);
        bullet.SetDamage(_damage);
        bullet.Rigidbody.velocity = (transform.forward * bullet.Speed);
        

    }

    public void Initialize(float damage)
    {
        _damage = damage;
    }
}
