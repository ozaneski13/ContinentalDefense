using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy_Movement))]
public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Stats")]
    [SerializeField] private int _healthPoint;
    public int HealthPoint => _healthPoint;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private int _prize;
    public int Prize => _prize;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [Header("Route")]
    [SerializeField] private int _defaultWay;
    public int DefaultWay => _defaultWay;

    [Header("Effects")]
    [SerializeField] private GameObject _enemyDeathEffect = null;

    [Header("Health Bar")]
    [SerializeField] private Image _healthBar = null;
    [SerializeField] private Color _criticalHealthColor = Color.red;

    private PlayerStats _playerStats = null;

    private int _currentHealthPoint;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        _currentHealthPoint = _healthPoint;
    }

    public void GetHit(int damage)
    {
        if (_currentHealthPoint > damage)
        {
            _currentHealthPoint -= damage;

            if (_currentHealthPoint <= _healthPoint / 2)
                _healthBar.color = _criticalHealthColor;

            _healthBar.fillAmount = _currentHealthPoint / (float)_healthPoint;
        }

        else
        {
            _playerStats.MoneyChanged(_prize);

            GameObject deathEffect = Instantiate(_enemyDeathEffect, transform.position, Quaternion.identity, ParticleHolder.Instance.transform);

            _healthBar.fillAmount = 0;

            int killCount = PlayerPrefs.GetInt("killCount", 0);
            killCount++;
            PlayerPrefs.SetInt("killCount", killCount);

            Destroy(deathEffect, 5f);
            Destroy(gameObject);
        }
    }
}