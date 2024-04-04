using UnityEngine;
using VContainer;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private BulletsPool _bulletsPool;
    
    public void Shoot()
    {
        var bullet = _bulletsPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.Rigidbody.useGravity = false;
        bullet.transform.SetParent(null);
        bullet.Rigidbody.velocity = (transform.forward * bullet.Speed);
        
    }
}
