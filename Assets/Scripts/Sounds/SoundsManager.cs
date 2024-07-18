using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioSource _zombieAudioSource;

    [SerializeField] private AudioClip _shot;
    [SerializeField] private AudioClip _zombieAttack;
    
    public void PlayShot()
    {
        _playerAudioSource.clip = _shot;
        _playerAudioSource.Play();
    }

    public void PlayZombieAttack()
    {
        _zombieAudioSource.clip = _zombieAttack;
        _zombieAudioSource.Play();
    }
}
