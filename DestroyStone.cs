using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStone : MonoBehaviour, IDamageable
{
    [SerializeField] float _stoneHealth = 15;
    [SerializeField] bool _isDestroy;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] [Range(0.1f, 5f)] float _scaleExplosion = 1;
    [SerializeField] GameObject _goldPrefab;
    [SerializeField] int _returnScore = 100;
    [SerializeField] GameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    public void Damage(float hit)
    {
        if (!_isDestroy)
        {
            _stoneHealth -= hit;
            
            if (_stoneHealth <= 0)
            {
                _gameManager.AddScore(_returnScore);
                _isDestroy = true;
                GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity, transform.parent);
                explosion.transform.localScale = Vector3.one * _scaleExplosion ;
                StartCoroutine(DropGold());
                Destroy(gameObject,0.4f);
            }
        }
    }
    IEnumerator DropGold()
    {
        yield return new WaitForSeconds(0.3f);
        int count = Random.Range(1,5);
        for (int i = 0; i < count; i++)
        {
            int offset = Random.Range(-1, 1);
            GameObject gold = Instantiate(_goldPrefab, transform.position + new Vector3Int(offset, offset, 0), Quaternion.identity, transform.parent);
            gold.GetComponent<PowerUp>().FromStone = true;
        }
    }
}
