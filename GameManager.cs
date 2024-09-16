using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _totalGold, _score, _highScore;
    public int TotalGold { get {  return _totalGold; } }
    public int Score { get { return _score; } }
    public int HighScore { get { return _highScore; } }

    [SerializeField] UiManager _uiManager;
    [SerializeField] float _obstacleMoveSpeed = 4f;
    public float ObstacleMoveSpeed { get { return _obstacleMoveSpeed;} }
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
        if (_uiManager == null) { return; }
        AudioManager.Instance.RiverSound();
        FetchData();

    }
    public void AddGold(int gold)
    {
        _totalGold += gold;
        _uiManager.GoldUI(_totalGold);
        PlayerPrefs.SetInt("TotalGold", _totalGold);
    }
    public void AddScore(int score)
    {
        _score += score;
        _uiManager.ScoreUI(_score);
        if (_score >= _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }
    void FetchData()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);

        _totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        _uiManager.GoldUI(_totalGold);
    }
}
