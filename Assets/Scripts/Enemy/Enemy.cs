using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy_Movement))]
public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Stats")]
    [SerializeField] private EEnemy _enemyType = EEnemy.Normal;
    public EEnemy EnemyType => _enemyType;

    [SerializeField] private int _healthPoint;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private int _prize;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [Header("Effects")]
    [SerializeField] private GameObject _enemyDeathParticle = null;
    private GameObject _deathEffect = null;

    [Header("Health Bar")]
    [SerializeField] private Image _healthBar = null;
    [SerializeField] private Color _normalHealthColor = Color.green;
    [SerializeField] private Color _criticalHealthColor = Color.red;

    private WaveSpawner _waveSpawner = null;
    public WaveSpawner WaveSpawner=>_waveSpawner;

    private PlayerStats _playerStats = null;

    private List<ParticleSystem> _particleSystems = null;

    private int _currentHealthPoint;

    private void Awake()
    {
        _deathEffect = Instantiate(_enemyDeathParticle, ParticleHolder.Instance.transform.position, Quaternion.identity, ParticleHolder.Instance.transform);
        
        _particleSystems = _deathEffect.GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void OnEnable()
    {
        _playerStats = PlayerStats.Instance;

        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Stop();

        _currentHealthPoint = _healthPoint;
        _healthBar.fillAmount = 1f;
        _healthBar.color = _normalHealthColor;
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

            _deathEffect.transform.position = transform.position;
            _deathEffect.SetActive(true);

            foreach (ParticleSystem particleSystem in _particleSystems)
                particleSystem.Play();

            _healthBar.fillAmount = 0;

            int killCount = PlayerPrefs.GetInt("killCount", 0);
            killCount++;
            PlayerPrefs.SetInt("killCount", killCount);

            _waveSpawner.RefillEnemy(this);
            gameObject.SetActive(false);
        }
    }

    public void SetSpawner(WaveSpawner spawner)
    {
        _waveSpawner = spawner;
    }
}