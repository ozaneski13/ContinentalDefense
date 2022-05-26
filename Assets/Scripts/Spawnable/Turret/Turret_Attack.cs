using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class Turret_Attack : Turret_Control
{
    [Header("Fire Ammunation")]
    [SerializeField] private GameObject _ammunationPrefab = null;

    [Header("Pool Options")]
    [SerializeField] private Transform _poolParent = null;
    [SerializeField] private float _poolControlDuration = 2f;
    [SerializeField] private int _poolSize = 100;
    [SerializeField] private int _refillCount = 20;

    private AttackerTurret _attackerTurret = null;

    private float _fireRate = 0f;

    private List<GameObject> _ammoPool = null;

    private int _firePointsIndex = 0;

    private void Start()
    {
        InitAmmunationPool();

        _attackerTurret = _turret as AttackerTurret;

        _fireRate = _attackerTurret.FireRate;
        _fireCountdown = 1f / _fireRate;

        StartCoroutine(CheckPool());
    }

    private void Update()
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            if(_canReturnStartingPosition)
                TurnToStartingRotation();

            return;
        }

        RotateToEnemy();
        
        if (_fireCountdown <= 0f)
        {
            FireAmmo();
            
            _fireCountdown = 1f / _fireRate;
        }
        
        _fireCountdown -= Time.deltaTime;
    }

    private void InitAmmunationPool()
    {
        _ammoPool = new List<GameObject>();

        FillPool();
    }

    private void FireAmmo()
    {
        int cannonCount = _attackerTurret.CannonCount;

        for (int i = 0; i < cannonCount; i++)
        {
            GameObject ammo = _ammoPool[_ammoPool.Count - 1];
            ammo.transform.position = _firePoints[i].position;
            ammo.transform.rotation = _firePoints[i].rotation;

            _ammoPool.Remove(ammo);

            ammo.SetActive(true);
            ammo.GetComponent<Ammunition>().SetTarget(_target);
            ammo.GetComponent<Ammunition>().SetAttacker(this);
        }
    }

    private IEnumerator CheckPool()
    {
        while (true)
        {
            if (_ammoPool.Count < _refillCount)
                FillPool();

            yield return new WaitForSeconds(_poolControlDuration);
        }
    }

    private void FillPool()
    {
        for (int i = _ammoPool.Count; i < _poolSize; i++)
        {
            GameObject poolFiller = Instantiate(_ammunationPrefab, _poolParent.position, _poolParent.rotation, _poolParent);
            poolFiller.SetActive(false);

            _ammoPool.Add(poolFiller);

            if (_firePointsIndex < _firePoints.Count - 1)
                _firePointsIndex++;
            else if (_firePointsIndex == _firePoints.Count - 1)
                _firePointsIndex = 0;
        }
    }

    public void RefillPool(GameObject ammo)
    {
        _ammoPool.Add(ammo);
    }
}