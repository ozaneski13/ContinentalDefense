using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 1f;

    [SerializeField] private bool _startsLate = false;
    [SerializeField] private float _lateStartTimer = 0f;

    [Header("Route")]
    [SerializeField] private int _defaultWay = 0;

    private float _countdown = 2f;

    private Transform _enemyParent = null;

    private List<GameObject[]> _waves = null;

    private int _waveNumber = 1;
    public int WaveNumber => _waveNumber;

    private bool _isAllWavesSpawned = false;
    private bool _currentWaveSpawned = true;

    public Action AllWavesSpawned;

    private void Start()
    {
        _waves = new List<GameObject[]>();

        _enemyParent = EnemyHolder.Instance.transform;
    }

    private void Update()
    {
        if (_countdown <= 0f && _waveNumber <= _maxWaveNumber && _currentWaveSpawned)
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
        _currentWaveSpawned = false;

        if (_startsLate)
            yield return new WaitForSeconds(_lateStartTimer);

        GameObject[] enemies = new GameObject[_waveNumber];

        for (int i = 0; i < _waveNumber; i++)
        {
            enemies[i] = SpawnEnemy();
            
            yield return new WaitForSeconds(_timeBetweenEachSpawn);
        }

        _waves.Add(enemies);

        _waveNumber++;

        _currentWaveSpawned = true;

        yield return new WaitForSeconds(_timeBetweenWaves);
    }

    private GameObject SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _enemyParent);
        enemy.transform.position = _spawnPoint.position;
        enemy.GetComponent<Enemy_Movement>().SetDefaultWay(_defaultWay);

        return enemy;
    }

    public int GetWaveStatus()
    {
        int finishedWaves = 0;

        foreach(GameObject[] enemies in _waves)
        {
            int enemyCounter = 0;

            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                    enemyCounter++;
            }

            if (enemyCounter == enemies.Length)
                finishedWaves++;
        }

        return finishedWaves;
    }
}