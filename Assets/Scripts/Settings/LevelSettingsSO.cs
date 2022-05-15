using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LevelSettings
{
    [SerializeField] private int _buildIndex = 0;
    public int BuildIndex => _buildIndex;

    [SerializeField] private int _waveCount = 100;
    public int WaveCount => _waveCount;

    [SerializeField] private int _startingMoney = 1000;
    public int StartingMoney => _startingMoney;

    [SerializeField] private int _startingHealth = 100;
    public int StartingHealth => _startingHealth;

    [SerializeField] private List<GameObject> _enemies = null;
    public List<GameObject> Enemies => _enemies;

    [SerializeField] private List<GameObject> _bosses = null;
    public List<GameObject> Bosses => _bosses;
}

[CreateAssetMenu(fileName = "LevelSettingsSO", menuName = "Level/Create LevelSettingsSO", order = 1)]
public class LevelSettingsSO : ScriptableObject
{
    public List<LevelSettings> LevelSettings;

    public LevelSettings GetLevelSettingsByLevelID(int buildIndex)
    {
        LevelSettings levelSet = LevelSettings.FirstOrDefault(i => i.BuildIndex == buildIndex);

        return levelSet;
    }
}