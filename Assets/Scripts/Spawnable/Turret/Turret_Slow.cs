using UnityEngine;

[RequireComponent(typeof(Turret))]
public class Turret_Slow : Turret_Control
{
    [Header("Fire Laser")]
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private ParticleSystem _laserImpactEffect = null;
    [SerializeField] private Light _pointLight = null;

    private SlowerTurret _slowerTurret = null;

    private Transform _tempTarget = null;

    private float _slowRate = 0f;

    private int _cannonCount = 0;

    private void Start()
    {
        _slowerTurret = _turret as SlowerTurret;

        _slowRate = _slowerTurret.SlowRate;

        _cannonCount = _slowerTurret.CannonCount;
    }

    private void Update()
    {
        if (_target == null)
        {
            ToggleLaser(false);

            if(_canReturnStartingPosition)
                TurnToStartingRotation();

            return;
        }

        RotateToEnemy();

        FireLaser();
    }

    private void ToggleLaser(bool status)
    {
        if (status)
        {
            _laserImpactEffect.gameObject.SetActive(status);

            if (!_laserImpactEffect.isPlaying)
                _laserImpactEffect.Play();
        }

        else
        {
            _laserImpactEffect.Stop();
            _laserImpactEffect.gameObject.SetActive(status);
        }
        
        _pointLight.enabled = status;
        _lineRenderer.enabled = status;
    }

    private void FireLaser()
    {
        ToggleLaser(true);

        _lineRenderer.SetPosition(0, _firePoints[_cannonCount - 1].position);
        _lineRenderer.SetPosition(1, _target.position);

        Vector3 direction = _firePoints[_cannonCount - 1].position - _target.position;

        _laserImpactEffect.transform.position = _target.position + direction.normalized;
        _laserImpactEffect.transform.rotation = Quaternion.LookRotation(direction);

        SlowEnemy();
    }

    private void SlowEnemy()
    {
        if (_tempTarget != _target)
        {
            if (_tempTarget != null)
                _tempTarget.GetComponent<Enemy_Movement>().SlowStoped(this);

            _tempTarget = _target;
            _tempTarget.GetComponent<Enemy_Movement>().GetSlowed(_slowRate, this);
        }
    }
}