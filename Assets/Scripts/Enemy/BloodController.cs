using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class BloodController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _prefabBlood;
    [SerializeField] private int _delay = 3000;
    private ObjectPool<GameObject> _bloodPool;

    private void Awake()
    {
        _bloodPool = new ObjectPool<GameObject>(Create, Get, Release);
    }

    private void Release(GameObject blood)
    {
        blood.SetActive(false);
    }

    private void Get(GameObject blood)
    {
        blood.SetActive(true);
    }

    private GameObject Create()
    {
        var blood = Instantiate(_prefabBlood);
        return blood;
    }

    public async UniTask SpawnBlood(Vector3 positionForSpawn)
    {
        var blood = _bloodPool.Get();
        blood.transform.position = positionForSpawn;
        await UniTask.Delay(_delay);
        _bloodPool.Release(blood);
    }

    public void ShowBlood()
    {
        _particleSystem.Play();
    }
    
}