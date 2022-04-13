using UnityEngine;

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
            _currentHealthPoint -= damage;

        else
        {
            _playerStats.MoneyChanged(_prize);

            GameObject deathEffect = Instantiate(_enemyDeathEffect, transform.position, Quaternion.identity);

            Destroy(deathEffect, 5f);
            Destroy(gameObject);
        }
    }

    public void GetSlowed()
    {

    }
}