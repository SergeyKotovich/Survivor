using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioClip _shot;
    [SerializeField] private AudioSource _audioSource;

    public void PlayShot()
    {
        _audioSource.clip = _shot;
        _audioSource.Play();
    }
}
