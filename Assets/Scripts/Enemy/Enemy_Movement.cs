using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Enemy_Movement : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Enemy _enemy = null;

    [Header("Enemy Model")]
    [SerializeField] private Transform _enemyModel = null;

    private PlayerStats _playerStats = null;

    private WaypointController _wayPointController = null;

    private List<Turret_Slow> _slowerTurrets = null;

    private Transform _target = null;

    private int _wayPointIndex = 0;
    private int _defaultWay = 0;

    private float _defaultSpeed = 0f;
    private float _currentSpeed = 0f;
   
    private bool _targetSet = false;
    private bool _defaultWaySet = false;

    private void OnEnable()
    {
        _wayPointController = WaypointController.Instance;

        _wayPointIndex = 0;

        _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
        _enemyModel.LookAt(_target);
        _targetSet = true;

        _defaultSpeed = _enemy.Speed;
        _currentSpeed = _defaultSpeed;
    }

    private void Start()
    {
        _playerStats = PlayerStats.Instance;
        _wayPointController = WaypointController.Instance;

        _slowerTurrets = new List<Turret_Slow>();

        _defaultSpeed = _enemy.Speed;
        _currentSpeed = _defaultSpeed;

        if(!_defaultWaySet)
            _defaultWay = _enemy.DefaultWay;

        _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
        _enemyModel.LookAt(_target);
        _targetSet = true;
    }

    private void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
            GetNextWayPoint();
    }

    private void GetNextWayPoint()
    {
        if (_wayPointIndex >= _wayPointController.WayPointsArray[_defaultWay].WayPointsList.Count - 1)
        {
            _playerStats.HealthChanged(_enemy.Damage);
            _enemy.WaveSpawner.RefillEnemy(_enemy);
            _enemy.gameObject.SetActive(false);

            return;
        }

        _wayPointIndex++;
        _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
        _enemyModel.LookAt(_target);
    }

    public void SetDefaultWay(int way)
    {
        _defaultWay = way;
        _defaultWaySet = true;

        if (!_targetSet)
            return;

        else
        {
            _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
            _targetSet = true;
        }
    }

    public void GetSlowed(float slowRate, Turret_Slow slowerTurret)
    {
        _slowerTurrets.Add(slowerTurret);

        _currentSpeed = _currentSpeed * (1f - slowRate);
    }

    public void SlowStoped(Turret_Slow slowerTurret)
    {
        _slowerTurrets.Remove(slowerTurret);

        if (_slowerTurrets.Count > 0)
            return;

        _currentSpeed = _defaultSpeed;
    }
}