using UnityEngine;

public class Turret_Slow : Turret_Control
{
    [Header("Fire Laser")]
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private ParticleSystem _laserImpactEffect = null;
    [SerializeField] private Light _pointLight = null;

    private void Update()
    {
        if (_target == null)
        {
            ToggleLaser(false);

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

        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _target.position);

        Vector3 direction = _firePoint.position - _target.position;

        _laserImpactEffect.transform.position = _target.position + direction.normalized;
        _laserImpactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }
}