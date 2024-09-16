using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text _scoreText, _totalGoldText, _lifeText;
    [SerializeField] Image _healthImage, _fuelImage;
    [SerializeField] GameObject _gameOverPanel, _resumePanel, _gameplayPanel;
    [SerializeField] Text _currentScoreText, _highScoreText, _goldTextAtGameOver;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameOverPanel.SetActive(false);
        _resumePanel.SetActive(false);
        _gameplayPanel.SetActive(true);

    }

    public void FuelUI(float fuel)
    {
        _fuelImage.fillAmount = fuel/100;
    }
    public void HealthUI(float health)
    {
        _healthImage.fillAmount = health / 100;
    }
    public void LifeUI(int life)
    {
        _lifeText.text = life.ToString();
    }
    public void ScoreUI(int score)
    {
        _scoreText.text = "Score : " + score.ToString();
    }
    public void GoldUI(int gold)
    {
        _totalGoldText.text = gold.ToString();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    public void ReplayButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void MainMenuButton()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = false;
        }
        SceneManager.LoadScene(0);
    }
    public void ResumeButton()
    {
        _resumePanel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = false;
        }
    }
    public void GameOver()
    {
        Cursor.visible = true;
        _goldTextAtGameOver.text = _gameManager.TotalGold.ToString();
        _currentScoreText.text = "Current Score : " + _gameManager.Score.ToString();
        _highScoreText.text = "High Score : " + _gameManager.HighScore.ToString();
        _gameOverPanel.SetActive(true);
        _gameplayPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape ))
        {
            AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = true;
            }
            Cursor.visible = true;
            _resumePanel.SetActive(true);
            Time.timeScale = 0;
            AudioManager.Instance.RiverSound();

        }
    }
}