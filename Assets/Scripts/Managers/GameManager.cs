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

    [Header("Wave Manager")]
    [SerializeField] private WaveManager _waveManager = null;

    [Header("UI")]
    [SerializeField] private GameObject _winUI = null;
    [SerializeField] private GameObject _gameOverUI = null;

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
        _waveManager.AllSpawnsDone += EndGame;
    }

    private void UnregisterFromEvents()
    {
        _playerStats.NoHealthRemain -= EndGame;
        _waveManager.AllSpawnsDone -= EndGame;
    }

    private void EndGame(bool isWin)
    {
        if (isWin)
        {
            int nextLevel = PlayerPrefs.GetInt("levelReached") + 1;
            PlayerPrefs.SetInt("levelReached", nextLevel);
            _winUI.SetActive(true);
        }

        else
            _gameOverUI.SetActive(true);

        GameEnded?.Invoke();
    }
}