using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _explosionAudio;
    void Start()
    {
        int i = Random.Range(0, _explosionAudio.Length-1);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = AudioManager.Instance.SFXAudio.volume;
        _audioSource.clip = _explosionAudio[i];
        _audioSource.Play();
    }
}
