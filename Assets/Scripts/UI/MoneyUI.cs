using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{    
    [SerializeField] private Text _moneyText = null;

    private PlayerStats _playerStats = null;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        UpdateMoney();

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _playerStats.MoneyCountChanged += UpdateMoney;
    }

    private void UnregisterFromEvents()
    {
        _playerStats.MoneyCountChanged -= UpdateMoney;
    }

    private void UpdateMoney()
    {
        _moneyText.text = "$" + _playerStats.Money.ToString();
    }
}