using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitStats();

        _money = _startMoney;
    }
    #endregion

    private LevelSettingsSO _levelSettingsSO = null;
    private LevelSettings _levelSettings = null;

    private int _health = 100;
    public int Health => _health;

    private int _startMoney = 0;

    private int _money = 0;
    public int Money => _money;

    public Action MoneyCountChanged;

    public Action HealthCountChanged;
    public Action<bool> NoHealthRemain;

    private void InitStats()
    {
        _levelSettingsSO = LevelSettingsHolder.Instance.LevelSettingsSO;
        _levelSettings = _levelSettingsSO.GetLevelSettingsByLevelID(SceneManager.GetActiveScene().buildIndex);

        _health = _levelSettings.StartingHealth;
        _startMoney= _levelSettings.StartingMoney;
    }

    public void MoneyChanged(int value)
    {
        _money += value;

        MoneyCountChanged?.Invoke();
    }

    public void HealthChanged(int value)
    {
        _health -= value;

        HealthCountChanged?.Invoke();

        if (_health == 0)
            NoHealthRemain?.Invoke(false);
    }
}