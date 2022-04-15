using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [Header("Prefab Options")]
    [SerializeField] private GameObject _enemyPrefab = null;

    [SerializeField] private Transform _spawnPoint = null;

    [Header("UI")]
    [SerializeField] private Text _countdownText = null;

    [Header("Wave Options")]
    [SerializeField] private int _maxWaveNumber = 100;

    [SerializeField] private float _countdown = 2f;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 1f;

    [SerializeField] private bool _startsLate = false;
    [SerializeField] private float _lateStartTimer = 0f;

    [Header("Route")]
    [SerializeField] private int _defaultWay = 0;

    private Transform _enemyParent = null;

    private int _waveNumber = 1;
    public int WaveNumber => _waveNumber;

    private bool _isAllWavesSpawned = false;

    public Action AllWavesSpawned;

    private void Start()
    {
        _enemyParent = EnemyHolder.Instance.transform;
    }

    private void Update()
    {
        if (_countdown <= 0f && _waveNumber <= _maxWaveNumber)
        {
            StartCoroutine(SpawnWave());
            _countdown = _timeBetweenWaves;
        }

        else if (_waveNumber > _maxWaveNumber && !_isAllWavesSpawned)
        {
            _isAllWavesSpawned = true;

            _countdown = 0;

            if (_countdownText != null)
                _countdownText.text = _countdown.ToString();

            StopAllCoroutines();

            AllWavesSpawned?.Invoke();

            return;
        }

        _countdown -= Time.deltaTime;

        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);

        _countdownText.text = string.Format("{0:00.0}", _countdown);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave()
    {
        if(_startsLate)
            yield return new WaitForSeconds(_lateStartTimer);

        for (int i = 0; i < _waveNumber; i++)
        {
            SpawnEnemy();
            
            yield return new WaitForSeconds(_timeBetweenEachSpawn);
        }

        _waveNumber++;

        yield return new WaitForSeconds(_timeBetweenWaves);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _enemyParent, _spawnPoint);
        enemy.GetComponent<Enemy_Movement>().SetDefaultWay(_defaultWay);
    }
}