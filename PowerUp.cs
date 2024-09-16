using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType _powerUpType;
    [SerializeField] int _value;
    [SerializeField] float _speed;
    [SerializeField] GameManager _gameManager;
    [SerializeField] bool _fromStone;
    public bool FromStone { set { _fromStone = value; } }
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _speed = _gameManager.ObstacleMoveSpeed;
    }
    private void Update()
    {
        if (!_fromStone)
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Collide With Player");
            Player player = collision.GetComponent<Player>();
            GameManager gameManager = FindObjectOfType<GameManager>();

            switch (_powerUpType)
            {
                case PowerUpType.Fuel:
                    //Debug.Log("fuel");
                    player.AddFuel();
                    Destroy(gameObject);
                    AudioManager.Instance.PowerUpAudio();
                    break;
                case PowerUpType.Gold:
                    //Debug.Log("Gold");
                    _value = Random.Range(1, 5) * 100;
                    gameManager.AddGold(_value);
                    Destroy(gameObject);
                    AudioManager.Instance.PowerUpAudio();
                    break;
                case PowerUpType.Life:
                    //Debug.Log("fuel");
                    _value = 1; 
                    player.AddLife(_value);
                    Destroy(gameObject);
                    AudioManager.Instance.PowerUpAudio();
                    break;
                default:
                    break;
            }
        }
    }
}
