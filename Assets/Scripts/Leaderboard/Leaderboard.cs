using System;
using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    private const string MAX_SCORE_KEY = "MaxScore";
    private const string NAME_LB = "Leaderboard";
    private int _maxScore;
    private LeaderboardYG _leaderboardYg;

    private void Awake()
    {
        _leaderboardYg = GetComponent<LeaderboardYG>();
        _maxScore = PlayerPrefs.GetInt(MAX_SCORE_KEY, 0);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveCurrentScore(int newScore)
    {
        if (newScore > _maxScore)
        {
            _maxScore = newScore;
            PlayerPrefs.SetInt(MAX_SCORE_KEY, _maxScore);
            PlayerPrefs.Save();
            YandexGame.NewLeaderboardScores(NAME_LB, newScore);
            _leaderboardYg.NewScore(_maxScore);
            _leaderboardYg.UpdateLB();
        }
    }
}