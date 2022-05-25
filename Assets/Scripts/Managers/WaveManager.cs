using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveSpawner> _waveSpawners = null;

    private int _allWavesSpawnedCount = 0;

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
            waveSpawner.AllWavesSpawned += AllWavesSpawned;
    }

    private void UnregisterFromEvents()
    {
        foreach (WaveSpawner waveSpawner in _waveSpawners)
            waveSpawner.AllWavesSpawned -= AllWavesSpawned;
    }

    private void AllWavesSpawned()
    {
        _allWavesSpawnedCount++;

        if (_allWavesSpawnedCount == _waveSpawners.Count)
            AllSpawnsDone?.Invoke(true);
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