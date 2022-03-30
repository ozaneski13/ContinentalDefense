using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private Transform _enemyParent = null;
    [SerializeField] private Transform _spawnPoint = null;

    [SerializeField] private int _maxWaveNumber = 100;

    [SerializeField] private int _defaultWay = 0;

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 1f;

    private IEnumerator _spawnRoutine = null;

    private int _waveNumber = 1;

    private float _countdown = 2f;

    private bool _spawnStarted = false;

    private void Awake()
    {
        _spawnRoutine = SpawnWave();
    }

    private void Update()
    {
        if (_countdown <= 0f && !_spawnStarted)
            StartCoroutine(_spawnRoutine);

        _countdown -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        StopCoroutine(_spawnRoutine);
    }

    private IEnumerator SpawnWave()
    {
        _spawnStarted = true;

        while (_waveNumber <= _maxWaveNumber)
        {
            for (int i = 0; i < _waveNumber; i++)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(_timeBetweenEachSpawn);
            }

            _waveNumber++;

            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _enemyParent, _spawnPoint);
        enemy.GetComponent<Enemy_Movement>().SetDefaultWay(_defaultWay);
    }
}