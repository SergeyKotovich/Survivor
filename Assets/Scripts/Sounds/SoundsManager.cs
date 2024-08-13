using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _effects;

    [SerializeField] private AudioClip[] _menuMusic;
    [SerializeField] private AudioClip[] _gameMusic;
    [SerializeField] private AudioClip _endMusic;
    [SerializeField] private AudioClip _startWave;
    [SerializeField] private AudioClip _shot;
    [SerializeField] private AudioClip _zombieAttack;
    [SerializeField] private AudioClip _coinCollect;
    [SerializeField] private AudioClip _improvement;
    [SerializeField] private AudioClip _heal;
    [SerializeField] private AudioClip _notEnoughMoney;

    private int _currentMenuIndex;
    private int _currentGameIndex;
    private bool _gameFinished;
    private bool _isMenu;

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

    private void Update()
    {
        if (!_music.isPlaying && !_gameFinished)
        {
            if (_isMenu)
            {
                SetNextMenuTrack();
            }
            else
            {
                SetNextGameTrack();
            }
        }
    }

    public void PlayImprovementsSound()
    {
        PlayClip(_effects, _improvement);
    }

    public void PlayNotEnoughMoney()
    {
        PlayClip(_effects, _notEnoughMoney);
    }

    public void PlayHealSound()
    {
        PlayClip(_effects, _heal);
    }

    public void PlayCoinCollect()
    {
        PlayClip(_effects, _coinCollect);
    }

    public void PlayShot()
    {
        PlayClip(_effects, _shot);
    }

    public void PlayMusicGame()
    {
        _isMenu = false;
        _gameFinished = false;
        _currentGameIndex = Random.Range(0, _gameMusic.Length);
        _music.clip = _gameMusic[_currentGameIndex];
        _music.Play();
    }

    public void PlayZombieAttack()
    {
        PlayClip(_effects, _zombieAttack);
    }

    public void PlayEndGame()
    {
        _gameFinished = true;
        _music.clip = _endMusic;
        _music.loop = false;
        _music.Play();
    }

    public void PlayStartWave()
    {
        PlayClip(_effects, _startWave);
        _gameFinished = false;
    }

    private void SetNextGameTrack()
    {
        _currentGameIndex = (_currentGameIndex + 1) % _gameMusic.Length;
        _music.clip = _gameMusic[_currentGameIndex];
        _music.Play();
    }

    private void SetNextMenuTrack()
    {
        _currentMenuIndex = (_currentMenuIndex + 1) % _menuMusic.Length;
        _music.clip = _menuMusic[_currentMenuIndex];
        _music.Play();
    }

    private void PlayClip(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    private void PlayMenuMusic()
    {
        _isMenu = true;
        _currentMenuIndex = Random.Range(0, _menuMusic.Length);
        _music.clip = _menuMusic[_currentMenuIndex];
        _music.Play();
    }
}