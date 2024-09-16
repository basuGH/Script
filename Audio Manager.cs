using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager  _instance;
    public static AudioManager Instance { get { return _instance; } }
    public AudioSource BackgroundAudio { get; private set; }
    public AudioSource SFXAudio { get; private set; }
    [SerializeField] AudioClip _playerBulletAudio, _riverSound, _powerUpAudio;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _instance = this;
        BackgroundAudio = GameObject.FindWithTag("BackgroundAudio").GetComponent<AudioSource>();
        SFXAudio = GameObject.FindWithTag("SFXAudio").GetComponent <AudioSource>();
    }

    public void PlayerGunAudio()
    {
        SFXAudio.PlayOneShot(_playerBulletAudio);
        if (Input.GetMouseButtonUp(0))
        {
            SFXAudio.Stop();
        }
    }
    public void PowerUpAudio()
    {
        SFXAudio.PlayOneShot(_powerUpAudio);
    }
    public void RiverSound()
    {
        BackgroundAudio.clip = _riverSound;
        BackgroundAudio.loop = true;
        BackgroundAudio.Play();
    }
}
