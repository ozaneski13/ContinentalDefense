using System.Collections;
using UnityEngine;

public class Turret_Attack : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Transform _partToRotate = null;

    [SerializeField] private float _range = 15f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _timeToCheckTarget = 0.5f;

    [Header("Fire")]
    [SerializeField] private Turret _turret = null;

    [SerializeField] private Transform _firePoint = null;

    [SerializeField] private GameObject _bulletPrefab = null;

    [SerializeField] private float _fireRate = 1f;

    private Transform _target = null;

    private IEnumerator _updateTargetRoutine = null;

    private float _fireCountdown = 0f;

    private void Start()
    {
        _updateTargetRoutine = UpdateTargetRoutine();
        StartCoroutine(_updateTargetRoutine);
    }

    private void Update()
    {
        if (_target == null)
        {
            TurnToStartingRotation();

            return;
        }

        RotateToEnemy();

        if (_fireCountdown <= 0f)
        {
            FireBullet();
            _fireCountdown = 1f / _fireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        StopCoroutine(_updateTargetRoutine);
    }

    private IEnumerator UpdateTargetRoutine()
    {
        GameObject[] enemies = null;
        GameObject nearestEnemy = null;

        while (true)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            nearestEnemy = null;

            float shortestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= _range)
                _target = nearestEnemy.transform;
            else
                _target = null;

            yield return new WaitForSeconds(_timeToCheckTarget);
        }
    }

    private void RotateToEnemy()
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;

        _partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void TurnToStartingRotation()
    {
        Vector3 startDirection = new Vector3(0f, 90f, 0f);
        Quaternion startLookRotation = Quaternion.LookRotation(startDirection);
        Vector3 startRotation = Quaternion.Lerp(_partToRotate.rotation, startLookRotation, Time.deltaTime * (_rotationSpeed)).eulerAngles;

        _partToRotate.rotation = Quaternion.Euler(0f, startRotation.y, 0f);
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Ammunition>().SetTarget(_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}