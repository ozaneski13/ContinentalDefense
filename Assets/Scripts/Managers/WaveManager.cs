using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private List<WaveSpawner> _waveSpawners = null;

    [Header("Boss Wave UI")]
    [SerializeField] private GameObject _bossWaveUI = null;
    [SerializeField] private AnimationClip _animClip = null;

    private int _allWavesSpawnedCount = 0;

    private bool _isRoutineWorking = false;

    public Action<bool> AllSpawnsDone;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
        {
            waveSpawner.AllWavesSpawned += AllWavesSpawned;
            waveSpawner.BossWaveSpawned += BossWaveSpawned;
        }
    }

    private void UnregisterFromEvents()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
        {
            waveSpawner.AllWavesSpawned -= AllWavesSpawned;
            waveSpawner.BossWaveSpawned -= BossWaveSpawned;
        }
    }

    private void AllWavesSpawned()
    {
        _allWavesSpawnedCount++;

        if (_allWavesSpawnedCount == _waveSpawners.Count)
            AllSpawnsDone?.Invoke(true);
    }

    private void BossWaveSpawned()
    {
        if (_isRoutineWorking)
            return;

        StartCoroutine(BossRoutine());
    }

    private IEnumerator BossRoutine()
    {
        _isRoutineWorking = true;

        _bossWaveUI.SetActive(true);

        yield return new WaitForSeconds(_animClip.length);

        _bossWaveUI.SetActive(false);

        _isRoutineWorking = false;
    }

    public int GetCurrentWave()
    {
        int waveStatus = 0;

        foreach (WaveSpawner spawner in _waveSpawners)
        {
            if (waveStatus < spawner.WaveNumber)
                waveStatus = spawner.WaveNumber;
        }

        return waveStatus;
    }
}