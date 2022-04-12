using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _money = _startMoney;
    }
    #endregion

    [SerializeField] private int _health = 100;
    public int Health => _health;

    [SerializeField] private int _startMoney = 0;

    private int _money = 0;
    public int Money => _money;

    public Action MoneyCountChanged;

    public Action HealthCountChanged;
    public Action NoHealthRemain;

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
            NoHealthRemain?.Invoke();
    }
}