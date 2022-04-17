using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveSpawner> _waveSpawners = null;

    public int GetCurrentWave()
    {
        int waveStatus = 0;

        foreach (WaveSpawner spawner in _waveSpawners)
        {
            if (waveStatus < spawner.GetWaveStatus())
                waveStatus = spawner.GetWaveStatus();
        }

        return waveStatus;
    }
}