using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class EnemySpawnController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WavesData _wavesData;
    [Header("Assets")]
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _weakEnemyPrefab;
    [SerializeField] private GameObject _midEnemyPrefab;
    [SerializeField] private GameObject _heavyEnemyPrefab;
    [SerializeField] private GameState _gameState;
    [Header("Settings")]
    [SerializeField] private float _timeBetweenWaves = 10f;
    [SerializeField] private float _minTimeBetweenEnemies = 1f;
    [SerializeField] private float _maxTimeBetweenEnemies = 3f;
    [SerializeField] private float _startDelayToSpawnEnemies = 1.5f;
    [Header("Events")]
    [SerializeField] private UnityEvent OnWavesEnded;
    
    private int _waveNumber;
    private ObjectPool _weakEnemyPool;
    private ObjectPool _midEnemyPool;
    private ObjectPool _heavyEnemyPool;

    private void Start()
    {
        _weakEnemyPool = new ObjectPool();
        _weakEnemyPool.ObjectPrefab = _weakEnemyPrefab;

        _midEnemyPool = new ObjectPool();
        _midEnemyPool.ObjectPrefab = _midEnemyPrefab;

        _heavyEnemyPool = new ObjectPool();
        _heavyEnemyPool.ObjectPrefab = _heavyEnemyPrefab;

    }

    public void StartSpawningEnemies()
    {
        StartCoroutine(StartDelayToCreateEnemies());
    }

    public IEnumerator StartDelayToCreateEnemies()
    {
        yield return new WaitForSeconds(_startDelayToSpawnEnemies);
        StartCoroutine(CreateNewEnemyWave());
    }
    IEnumerator CreateNewEnemyWave()
    {
        while(_waveNumber < _wavesData.Waves.Length && _gameState.GamePlayingState == GameState.State.Playing){
            StartCoroutine(SpawnEnemiesFromPool(_wavesData.Waves[_waveNumber].WeakEnemies, _weakEnemyPool));
            StartCoroutine(SpawnEnemiesFromPool(_wavesData.Waves[_waveNumber].MidEnemies, _midEnemyPool));
            StartCoroutine(SpawnEnemiesFromPool(_wavesData.Waves[_waveNumber].HeavyEnemies, _heavyEnemyPool));
            while(GameManager.Instance.EnemiesOnScene > 0){
                yield return null;
            }
            _waveNumber++;
            yield return new WaitForSeconds(_timeBetweenWaves);
        }

        if(!GameManager.Instance.GameOver)
            OnWavesEnded?.Invoke();
    }

    IEnumerator SpawnEnemiesFromPool(int enemyAmount,ObjectPool objectPool)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemy = objectPool.GetGameObjectFromPool();
            enemy.transform.position = _spawnPoint.position;
            enemy.SetActive(true);
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenEnemies, _maxTimeBetweenEnemies));
        }
    }

    IEnumerator SpawnEnemies(int enemyAmount,GameObject enemyPrefab)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenEnemies, _maxTimeBetweenEnemies));
        }
    }


}
