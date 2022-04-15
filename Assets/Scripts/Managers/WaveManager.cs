using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveSpawner> _waveSpawners = null;

    public int GetCurrentWave()
    {
        int maxWaveCounter = 0;

        foreach (WaveSpawner waveSpawner in _waveSpawners)
            if (waveSpawner.WaveNumber > maxWaveCounter)
                maxWaveCounter = waveSpawner.WaveNumber;

        return maxWaveCounter;
    }
}