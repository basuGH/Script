using System.Collections;
using UnityEngine;
public class EnemyCanon : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] float _rotationSpeed = 40f;
    [SerializeField] float _targetYRange = 7;
    [SerializeField] bool _isFireRange;
    [SerializeField] bool _canFire;
    [SerializeField] Transform _target;
    [SerializeField] Animator _animator;
    [SerializeField] Transform _firePosition;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] AudioClip _bulletAudio;
    [SerializeField] AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = AudioManager.Instance.SFXAudio.volume;

        _canFire = true;
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        LookAtTarget();
        FireMethod();
    }
    private void LookAtTarget()
    {
        if (_player == null)
        {
            return;
        }
        if (!_player.IsAlive)
        {
            _target = transform;
            return; 
        }
        if (transform.position.y <= _targetYRange && transform.position.y >= _player.transform.position.y)
        {
            _target = _player.transform;
            _isFireRange = true;
        }
        else
        {
           _target = transform;
        }
        Vector3 distance = transform.position - _target.position;
        float angle = Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 79f)
        {
            _isFireRange = false;
            _rotationSpeed = 3f;
        }
        angle = Mathf.Clamp(-angle, -80f, 80f);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, _rotationSpeed * Time.deltaTime);
    }
    void FireMethod()
    {
        if (_isFireRange && _canFire)
        {
            _animator.SetTrigger("Fire");
            StartCoroutine(ResetFire());
        }
    }
    IEnumerator ResetFire()
    {
        float delay;
        delay = Random.Range(0.40f, 0.60f);
        yield return new WaitForSeconds(delay);

        _canFire = false;
        delay = Random.Range(1.5f, 2f);
        yield return new WaitForSeconds(delay);
        _canFire = true;
    }
    public void FireEvent()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePosition.transform.position, transform.rotation);
        _audioSource.PlayOneShot(_bulletAudio);
    }
}