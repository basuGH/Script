using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _rotationAmount = 20;
    [SerializeField] float _health = 100;
    [SerializeField] bool _isAlive = true;
    [SerializeField] GameObject _body, _canon, _damageBody;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] Transform _healthBar;
    [SerializeField] int _playerLife = 5;
    [SerializeField] float _fuel = 100f;
    [SerializeField] bool _noFuel;
    [SerializeField] bool _isFuelTankFull;
    [SerializeField] float _fuelDecreaseRate = 0.7f;
    [SerializeField] float _fuelIncreaseRate = 20f;
    [SerializeField] bool _shielActive;
    [SerializeField] GameObject _shield;
    [SerializeField] float _shieldDuration = 4f;
    [SerializeField] AudioSource _audioSource;
    public AudioSource AudioSource {  get { return _audioSource; }  }
    [SerializeField] UiManager _uiManager;
    public bool IsAlive { get { return _isAlive; } }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = AudioManager.Instance.SFXAudio.volume * 0.20f;

        _uiManager = FindObjectOfType<UiManager>();
        _audioSource.Play();
        ShielActivation();
        StartCoroutine(DecreaseFuel());
    }

    IEnumerator DecreaseFuel()
    {
        while (_isAlive)
        {
            if (!_noFuel)
            {
                _fuel -= Time.deltaTime * _fuelDecreaseRate;
                if (_fuel <= 0)
                {
                    _noFuel = true;

                }
            }
            _uiManager.FuelUI(_fuel);
            yield return null;
        }
    }
    public void AddFuel()
    {
        if (_isAlive && !_noFuel)
        {
            _isFuelTankFull = false;
            StartCoroutine(AddFuelSequence());
        }
    }
    public IEnumerator AddFuelSequence()
    {
        while (!_isFuelTankFull)
        {
            _fuel += Time.deltaTime * _fuelIncreaseRate;
            if (_fuel > 100)
            {
                _isFuelTankFull = true;
                _fuel = 100;
            }
            //Debug.Log("While IN");
            _uiManager.FuelUI( _fuel);
            yield return null;
        }
        //Debug.Log("While Out");
    }

    public void AddLife(int life)
    {
        if (_playerLife < 5)
        {
            _playerLife += life;
        }
        _uiManager.LifeUI(_playerLife);
    }
    public void Damage(float hit)
    {
        if (_isAlive && !_shielActive)
        {
            _health -= hit;
            if (_health <= 0)
            {
                DecreaseLife();
            }
            _uiManager.HealthUI(_health);
        }
    }

    private void DecreaseLife()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("CameraEffect");
        ShielActivation();
        transform.position = new Vector3(0, -4, 0);
        _playerLife--;
        _health = 100;
        _fuel = 100;

        if (_playerLife <= 0)
        {
            _uiManager.GameOver();
            _isAlive = false;
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity, transform);
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(explosion, 2f);
            StartCoroutine(InDamageProcess());
            _audioSource.Stop();
           //Destroy(gameObject, 5f);
        }
        _uiManager.HealthUI(_health);
        _uiManager.LifeUI(_playerLife);
    }

    void ShielActivation()
    {
        _shielActive = true;
        _shield.SetActive(true);
        StartCoroutine(ShieldDeactivation());
    }
    IEnumerator ShieldDeactivation()
    {
        yield return new WaitForSeconds(_shieldDuration);
        _shield.SetActive(false);
        _shielActive = false;
    }
    IEnumerator InDamageProcess()
    {
        yield return new WaitForSeconds(0.5f);
        _healthBar.gameObject.SetActive(false);
        _body.SetActive(false);
        _canon.SetActive(false);
        _damageBody.SetActive(true);
    }
    private void Update()
    {
        if (_isAlive && !_noFuel)
        {
            Move();
        }
        else
        {
            transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
        }
    }
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed * Time.deltaTime;

        transform.position += move;

        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -8f, 0));

        float rotation = horizontalInput * _rotationAmount;

        transform.localRotation = Quaternion.Euler(0, 0, -rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Land") || collision.CompareTag("Stone"))
        {
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity, collision.transform);
            Destroy(explosion, 3f);
            explosion.transform.localScale = Vector3.one * 0.5f; 
            DecreaseLife();
        }
    }
}