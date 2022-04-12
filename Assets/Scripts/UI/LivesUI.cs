using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private Text _livesText = null;

    private PlayerStats _playerStats = null;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        UpdateLives();

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _playerStats.HealthCountChanged += UpdateLives;
    }

    private void UnregisterFromEvents()
    {
        _playerStats.HealthCountChanged -= UpdateLives;
    }

    private void UpdateLives()
    {
        _livesText.text = _playerStats.Health.ToString() + " LIVES";
    }
}