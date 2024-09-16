using System.Collections;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyPrefab;
    [SerializeField] Transform _enemyManager;
    [SerializeField] Transform _obstacleManager;
    [SerializeField] Transform _powerUpManager;
    [SerializeField] float _xRange = 12;
    [SerializeField] Player _player;
    [SerializeField] Transform _grid;
    [SerializeField] GameObject _landPrefab;
    [SerializeField] GameObject[] _obstaclesPrefab;
    [SerializeField] GameObject[] _powerUpPrefab;
    private void Awake()
    {
        //LandSpawn();
    }
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        StartCoroutine(EnemySpawn());
        StartCoroutine(ObstacleSpawn());
        StartCoroutine(PowerUpSpawn());
    }
    IEnumerator EnemySpawn()
    {
        while (_player.IsAlive)
        {
            int i = Random.Range(0, _enemyPrefab.Length - 1);
            GameObject enemy = Instantiate(_enemyPrefab[i], new Vector2(Random.Range(-_xRange, _xRange), 20f), Quaternion.identity, _enemyManager);

            float delay = Random.Range(3f, 7f);
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator ObstacleSpawn()
    {
        while (_player.IsAlive)
        {
            float delay = Random.Range(7f, 12f);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, _obstaclesPrefab.Length - 1);
            GameObject obstacle = Instantiate(_obstaclesPrefab[i], 
                new Vector2(Random.Range(-_xRange - 4, _xRange - 4), 14f), 
                Quaternion.identity, _obstacleManager);
        }
    }
    IEnumerator PowerUpSpawn()
    {
        while (_player.IsAlive)
        {
            int i = Random.Range(0, _powerUpPrefab.Length - 1);
            GameObject powerup = Instantiate(_powerUpPrefab[i], new Vector2(Random.Range(-_xRange-5, _xRange-5), 10.7f), Quaternion.identity, _powerUpManager);

            float delay = Random.Range(5f, 12f);
            yield return new WaitForSeconds(delay);
        }
    }
    public void LandSpawn()
    {
        GameObject land = Instantiate(_landPrefab, transform.position + new Vector3(0, 28f, 0), Quaternion.identity, _grid);
    }
}