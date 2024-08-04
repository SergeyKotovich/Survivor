using System;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _zombieEffects;
    [SerializeField] private AudioSource _playerEffects;

    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private AudioClip _shot;
    [SerializeField] private AudioClip _zombieAttack;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        PlayMenuMusic();
    }

    public void PlayShot()
    {
        _playerEffects.clip = _shot;
        _playerEffects.Play();
    }

    public void PlayMusicGame()
    {
        _music.clip = _gameMusic;
        _music.Play();
    }

    public void PlayZombieAttack()
    {
        _zombieEffects.clip = _zombieAttack;
        _zombieEffects.Play();
    }

    private void PlayMenuMusic()
    {
        _music.clip = _menuMusic;
        _music.Play();
        _music.loop = true;
    }
}