using UnityEngine;
using UnityEngine.Pool;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletsHolder;
    private ObjectPool<Bullet> _pool;

    private void Start()
    {
        _pool = new ObjectPool<Bullet>(OnBulletCreate, OnGet, OnRelease);
    }
    
    private Bullet OnBulletCreate()
    {
        return Instantiate(_bulletPrefab, _bulletsHolder);
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
    }

    public Bullet GetBullet()
    {
        var bullet = _pool.Get();
        bullet.BulletHit += OnBulletHit;
        return bullet;
    }
    
    private void OnBulletHit(Bullet bullet)
    {
        bullet.BulletHit -= OnBulletHit;
        bullet.transform.SetParent(_bulletsHolder);
        _pool.Release(bullet);
    }
}