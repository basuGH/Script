using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float _speed;
    [SerializeField] bool _isAlive = true;
    [SerializeField] float _health = 10;
    [SerializeField] GameObject _body, _canon, _damageBody;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] float _hitPoint = 20f;
    [SerializeField] bool _canMove = true;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _engine1, _engine2;
    [SerializeField] GameManager _gameManager;
    [SerializeField] int _returnScore = 100;
    
    public void Damage(float hit)
    {
        if (_isAlive)
        {
            _health -= hit;
            if (_health <= 0)
            {
                _gameManager.AddScore(_returnScore);
                StartDamageProcess();
            }
        }
    }
    private void StartDamageProcess()
    {
        _isAlive = false;
        _audioSource.Stop();
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity, transform);
        Destroy(explosion, 2f);
        GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(InDamageProcess());
    }
    IEnumerator InDamageProcess()
    {
        yield return new WaitForSeconds(0.5f);
        _body.SetActive(false);
        _canon.SetActive(false);
        _damageBody.SetActive(true);
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = AudioManager.Instance.SFXAudio.volume;
        _gameManager = FindObjectOfType<GameManager>();
        _speed = Random.Range(4f, 9f);
        if (_speed < 7f)
        {
            _audioSource.clip = _engine1;
        }
        else
        {
            _audioSource.clip = _engine2;
        }
        _audioSource.Play();
    }
    private void Update()
    {
        if (_canMove)
        {
            transform.Translate(-Vector3.up * _speed * Time.deltaTime);
        }
        if (transform.position.y <= -12f)
        {
            if (_isAlive)
            {
                transform.position = new Vector2(Random.Range(-12f, 12f), 12);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAlive && _canMove)
        {
            if (collision.CompareTag("Shield"))
            {
                StartDamageProcess();
            }
            else if (collision.CompareTag("Player"))
            {
                IDamageable damageable = collision.GetComponent<IDamageable>();
                damageable.Damage(_hitPoint);
                StartDamageProcess();
            }
            else if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().StartDamageProcess();
                StartDamageProcess();
            }
            else if (collision.CompareTag("Land") || collision.CompareTag("Stone"))
            {
                StartDamageProcess();
                transform.parent = collision.transform;
                _canMove = false;
            }
        }
    }
}