using UnityEngine;

public class Turret_Attack : Turret_Control
{
    [Header("Fire Bullet")]
    [SerializeField] private GameObject _bulletPrefab = null;

    private AttackerTurret _attackerTurret = null;

    private float _fireRate = 0f;

    private void Start()
    {
        _attackerTurret = _turret as AttackerTurret;

        _fireRate = _attackerTurret.FireRate;
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

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Ammunition>().SetTarget(_target);
    }
}