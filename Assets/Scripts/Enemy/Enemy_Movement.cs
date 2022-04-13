using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Enemy _enemy = null;

    private PlayerStats _playerStats = null;

    private WaypointController _wayPointController = null;

    private Transform _target = null;

    private int _wayPointIndex = 0;
    private int _defaultWay = 0;

    private float _speed = 0f;
   
    private bool _targetSet = false;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;
        _wayPointController = WaypointController.Instance;

        _speed = _enemy.Speed;
        _defaultWay = _enemy.DefaultWay;

        _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
        _targetSet = true;
    }

    private void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (_wayPointIndex >= _wayPointController.WayPointsArray[_defaultWay].WayPointsList.Count - 1)
        {
            Destroy(gameObject);
            _playerStats.HealthChanged(_enemy.Damage);

            return;
        }

        _wayPointIndex++;
        _target = _wayPointController.WayPointsArray[0].WayPointsList[_wayPointIndex];
    }

    public void SetDefaultWay(int way)
    {
        _defaultWay = way;

        if (!_targetSet)
            return;
        else
        {
            _target = _wayPointController.WayPointsArray[_defaultWay].WayPointsList[_wayPointIndex];
            _targetSet = true;
        }
    }
}