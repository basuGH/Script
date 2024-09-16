using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LandMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] GameManager _gameManager;
    [SerializeField] bool _isLand;
    void Start()
    {
        _isLand = false;
        _gameManager = FindObjectOfType<GameManager>();
        _moveSpeed = _gameManager.ObstacleMoveSpeed;
        _spawnManager = FindObjectOfType<SpawnManager>();
    }
    void Update()
    {
        transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);
        if (transform.position.y <= -200 && !_isLand)
        {
            _isLand = true;
            _spawnManager.LandSpawn();
        }

        if (transform.position.y <= -230)
        {
            Destroy(gameObject);
        }
    }
}