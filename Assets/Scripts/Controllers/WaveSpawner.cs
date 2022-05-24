using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private Transform _spawnPoint = null;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _countdownText = null;

    [Header("Wave Options")]
    [SerializeField] private int _maxWaveNumber = 100;

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 1f;

    [Header("Route")]
    [SerializeField] private int _defaultWay = 0;

    [Header("Pool Settings")]
    [SerializeField] private Transform _poolParent = null;
    [SerializeField] private int _poolSize = 40;
    [SerializeField] private int _refillCount = 10;
    [SerializeField] private float _poolControlDuration = 0.5f;

    private LevelSettingsSO _levelSettingsSO = null;
    private LevelSettings _levelSettings = null;

    private List<GameObject> _enemyTypesList = new List<GameObject>();
    private List<GameObject> _enemyBossTypesList = new List<GameObject>();

    private Transform _enemyParent = null;

    private List<GameObject[]> _waves = null;
    private List<List<GameObject>> _pools = null;

    private float _countdown = 5f;

    private int _waveNumber = 0;

    private bool _currentWaveSpawned = true;
    private bool _isInvoked = false;

    public Action AllWavesSpawned;

    private void Start()
    {
        _waves = new List<GameObject[]>();

        _enemyParent = EnemyHolder.Instance.transform;

        _levelSettingsSO = LevelSettingsHolder.Instance.LevelSettingsSO;
        _levelSettings = _levelSettingsSO.GetLevelSettingsByLevelID(SceneManager.GetActiveScene().buildIndex);

        _maxWaveNumber = _levelSettings.WaveCount;
        _enemyTypesList = _levelSettings.Enemies;
        _enemyBossTypesList = _levelSettings.Bosses;

        InitEnemyPool();
    }

    private void Update()
    {
        if (_countdown <= 0f && _waveNumber < _maxWaveNumber && _currentWaveSpawned)
            StartCoroutine(SpawnWave());

        else if (_waveNumber >= _maxWaveNumber)
        {
            _countdown = 00.0f;

            if (_countdownText != null)
                _countdownText.text = string.Format("{0:00.0}", _countdown);

            StopAllCoroutines();

            if (GetWaveStatus() == _maxWaveNumber && !_isInvoked)
            {
                _isInvoked = true;
                AllWavesSpawned?.Invoke();
            }

            return;
        }

        if (_countdown > 0f)
            _countdown -= Time.deltaTime;

        _countdown = Mathf.Clamp(_countdown, 00.0f, Mathf.Infinity);

        if (_countdownText != null)
            _countdownText.text = string.Format("{0:00.0}", _countdown);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void InitEnemyPool()
    {
        _pools = new List<List<GameObject>>();

        FillPools();

        StartCoroutine(RefillRoutine());
    }

    private void FillPools()
    {
        for (int k = 0; k < (_enemyTypesList.Count + _enemyBossTypesList.Count); k++)
        {
            List<GameObject> pool = new List<GameObject>();

            FillPool(pool, k);

            _pools.Add(pool);
        }
    }

    private IEnumerator RefillRoutine()
    {
        while (true)
        {
            int index = 0;

            foreach (List<GameObject> pool in _pools)
            {
                if (pool.Count < _refillCount)
                    FillPool(pool, index);

                index++;
            }

            yield return new WaitForSeconds(_poolControlDuration);
        }
    }

    private void FillPool(List<GameObject> pool, int index)
    {
        for (int i = pool.Count; i < _poolSize; i++)
        {
            GameObject enemy;

            if (index < _enemyTypesList.Count)
                enemy = Instantiate(_enemyTypesList[index], _poolParent);

            else
                enemy = Instantiate(_enemyBossTypesList[index - _enemyBossTypesList.Count - 1], _poolParent);

            enemy.SetActive(false);
            enemy.transform.position = _spawnPoint.position;
            pool.Add(enemy);
        }
    }

    private IEnumerator SpawnWave()
    {
        _currentWaveSpawned = false;

        GameObject[] enemies = new GameObject[_waveNumber + 1];

        for (int i = 0; i < _waveNumber + 1; i++)
        {
            enemies[i] = SpawnEnemy();
            
            yield return new WaitForSeconds(_timeBetweenEachSpawn);
        }

        if (_enemyBossTypesList.Count != 0)
            StartCoroutine(CheckBossWave());

        _waves.Add(enemies);

        _waveNumber++;

        _currentWaveSpawned = true;

        if (_waveNumber == _maxWaveNumber)
            _countdown = 00.0f;
        else
            _countdown = _timeBetweenWaves;

        yield return new WaitForSeconds(_timeBetweenWaves);
    }

    private IEnumerator CheckBossWave()
    {
        int bossCount = 0;

        if (_waveNumber == _maxWaveNumber)
            bossCount = 16;
        else if (_waveNumber == _maxWaveNumber / 2)
            bossCount = 8;
        if (_waveNumber % 10 == 0 && _waveNumber != 0)
            bossCount = 4;

        int index = 0;

        for (int i = 0; i < bossCount; i++)
        {
            GameObject bossEnemy = _pools[_enemyTypesList.Count + index][_pools[_enemyTypesList.Count + index].Count - 1];
            bossEnemy.transform.parent = _enemyParent;

            bossEnemy.SetActive(true);

            bossEnemy.GetComponent<Enemy_Movement>().SetDefaultWay(_defaultWay);

            _pools[_enemyTypesList.Count + index].Remove(bossEnemy);

            if ((index + 1) == _enemyBossTypesList.Count)
                index = 0;
            else
                index++;

            yield return new WaitForSeconds(_timeBetweenEachSpawn * 2);
        }
    }

    private GameObject SpawnEnemy()
    {
        int enemyTypeIndex = UnityEngine.Random.Range(0, _enemyTypesList.Count);

        GameObject enemy = _pools[enemyTypeIndex][_pools[enemyTypeIndex].Count - 1];
        enemy.transform.parent = _enemyParent;

        _pools[enemyTypeIndex].Remove(enemy);

        enemy.SetActive(true);
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
            {
                finishedWaves++;

                int finishedWaveCount = PlayerPrefs.GetInt("finishedWaves", 0);
                finishedWaveCount++;
                PlayerPrefs.SetInt("finishedWaves", finishedWaveCount);
            }
        }

        return finishedWaves;
    }
}