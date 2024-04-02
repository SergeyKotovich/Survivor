using UnityEngine;
using VContainer;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private BulletsPool _bulletsPool;
    
    public void Shoot(Vector3 direction)
    {
        var bullet = _bulletsPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.Rigidbody.useGravity = false;
        bullet.Rigidbody.AddForce(direction * bullet.Speed);
        bullet.transform.SetParent(null);
    }
}
