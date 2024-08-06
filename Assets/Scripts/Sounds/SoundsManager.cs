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
    private int _currentIndex;

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
        if (!_music.isPlaying)
        {
            SetNextTrack();
        }
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
        _music.clip = _gameMusic[_currentIndex];
        _music.loop = true;
        _music.Play();
    }

    public void PlayZombieAttack()
    {
        PlayClip(_effects, _zombieAttack);
    }

    public void PlayEndGame()
    {
        _music.clip = _endMusic;
        _music.loop = false;
        _music.Play();
    }

    public void PlayStartWave()
    {
        PlayClip(_effects, _startWave);
    }

    private void SetNextTrack()
    {
        _music.clip = _gameMusic[_currentIndex];
        _music.Play();
        _currentIndex = (_currentIndex + 1) % _gameMusic.Length;
    }
    private void PlayClip(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    private void PlayMenuMusic()
    {
        if (_menuMusic.Length > 0)
        {
            var randomIndex = Random.Range(0, _menuMusic.Length);
            _music.clip = _menuMusic[randomIndex];
            _music.loop = true;
            _music.Play();
        }
    }
}
