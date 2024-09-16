using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu,_howToPlay, _settingsPanel, _creditsPanel;
    [SerializeField] Text _totalGoldText, _highScoreText;
    [SerializeField] Slider _volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        MainPanelActive();

        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        AudioManager.Instance.RiverSound();
    }

    private void MainPanelActive()
    {
        _mainMenu.SetActive(true);
        _howToPlay.SetActive(false);
        _settingsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _totalGoldText.text = PlayerPrefs.GetInt("TotalGold", 0).ToString();
        _highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void PlayBtnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void HowToPlayBtnClick()
    {
        _mainMenu.SetActive(false);
        _howToPlay.SetActive(true);
    }
    public void SettingBtnClick()
    {
        _mainMenu.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void QuitBtnClick()
    {
        Application.Quit();
    }
    public void CreditsBtnClick()
    {
        _mainMenu.SetActive(false);
        _creditsPanel.SetActive(true);
    }
    public void BackBtnClick()
    {
        MainPanelActive();
    }
    public void ResetBtnClick()
    {
        PlayerPrefs.DeleteAll();
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
    }
    public void VolumeSlider()
    {
        AudioManager.Instance.BackgroundAudio.volume = _volumeSlider.value;
        AudioManager.Instance.SFXAudio.volume = _volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", _volumeSlider.value);
    }
}