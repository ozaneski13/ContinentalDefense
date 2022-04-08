using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private int _healthPoint;
    public int HealthPoint => _healthPoint;

    [SerializeField] private float _speed;
    public float Speed => _speed;


    [SerializeField] private int _defaultWay;
    public int DefaultWay => _defaultWay;

    private int _currentHealthPoint;

    private void Start()
    {
        _currentHealthPoint = _healthPoint;
    }

    public void GetHit(int damage)
    {
        if (_currentHealthPoint > damage)
            _currentHealthPoint -= damage;

        else
            Destroy(gameObject);
    }
}