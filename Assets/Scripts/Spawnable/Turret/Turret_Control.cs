using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Control : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] protected Turret _turret = null;

    [Header("Rotation")]
    [SerializeField] protected bool _canReturnStartingPosition = false;
    [SerializeField] private Transform _partToRotate = null;
    
    private float _range = 0f;
    private float _rotationSpeed = 0f;
    private float _timeToCheckTarget = 0f;

    [Header("Fire Point")]
    [SerializeField] protected List<Transform> _firePoints = null;

    protected Transform _target = null;

    protected IEnumerator _updateTargetRoutine = null;

    protected float _fireCountdown = 0f;

    private void Awake()
    {
        _range = _turret.Range;
        _rotationSpeed = _turret.RotationSpeed;
        _timeToCheckTarget = _turret.TimeToCheckTarget;

        _updateTargetRoutine = UpdateTargetRoutine();
        StartCoroutine(_updateTargetRoutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(_updateTargetRoutine);
    }

    protected IEnumerator UpdateTargetRoutine()
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

    protected void RotateToEnemy()
    {
        Vector3 direction = _target.position - _partToRotate.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;

        _partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    protected void TurnToStartingRotation()
    {
        Vector3 startDirection = new Vector3(0f, 90f, 0f);
        Quaternion startLookRotation = Quaternion.LookRotation(startDirection);
        Vector3 startRotation = Quaternion.Lerp(_partToRotate.rotation, startLookRotation, Time.deltaTime * (_rotationSpeed)).eulerAngles;

        _partToRotate.rotation = Quaternion.Euler(0f, startRotation.y, 0f);
    }
}