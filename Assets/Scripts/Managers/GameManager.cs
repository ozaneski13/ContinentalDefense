using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [SerializeField] private GameObject _endGameUI = null;
    private PlayerStats _playerStats = null;

    public Action GameEnded;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _playerStats.NoHealthRemain += EndGame;
    }

    private void UnregisterFromEvents()
    {
        _playerStats.NoHealthRemain -= EndGame;
    }

    private void EndGame()
    {
        GameEnded?.Invoke();
        _endGameUI.SetActive(true);
    }
}