using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] float _moveSpeed;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _moveSpeed = _gameManager.ObstacleMoveSpeed;  
    }
    void Update()
    {
        transform.Translate ( Vector2.down * _moveSpeed * Time.deltaTime);
        if (transform.position.y <= -12 )
        {
            Destroy (gameObject);
        }
    }
}