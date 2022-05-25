using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INode
{
    [Header("Effects")]
    [SerializeField] protected GameObject _builtParticle = null;
    [SerializeField] protected GameObject _sellParticle = null;

    [Header("Node")]
    [SerializeField] protected Renderer _renderer = null;

    [SerializeField] protected Color _nodeOccupiedColor = Color.red;
    [SerializeField] protected float _nodeOccupiedTimer = 0.5f;

    [Header("Pool")]
    [SerializeField] private GameObject[] _spawnables = null;
    [SerializeField] private Transform _spawnablePoolParent = null;

    protected Color _startColor = Color.white;

    [Header("Positioning")]
    [SerializeField] protected Vector3 _positionOffset = new Vector3(0, 0.5f, 0);
    public Vector3 PositionOffset => _positionOffset;

    private List<Spawnable> _spawnablePool = null;

    protected PlayerStats _playerStats = null;

    protected Transform _particleHolder = null;

    protected Transform _attackedHolder = null;

    protected Spawnable _currentSpawnable = null;
    public Spawnable CurrentSpawnable => _currentSpawnable;

    private void Awake()
    {
        InitPool();
    }

    private void InitPool()
    {
        _spawnablePool = new List<Spawnable>();

        foreach(GameObject spawnable in _spawnables)
        {
            GameObject poolSpawnable = Instantiate(spawnable, transform.position + _positionOffset, transform.rotation);
            poolSpawnable.transform.parent = _spawnablePoolParent;
            poolSpawnable.SetActive(false);

            _spawnablePool.Add(poolSpawnable.GetComponent<Spawnable>());
        }
    }

    private void Start()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
        if (_renderer == null)
            _renderer = GetComponentInParent<Renderer>();

        _playerStats = PlayerStats.Instance;

        _particleHolder = ParticleHolder.Instance.transform;

        _attackedHolder = SpawnableHolder.Instance.transform;

        _startColor = _renderer.material.color;
    }

    public void NodeOccupied()
    {
        StartCoroutine(OccupiedRoutine());
    }

    protected IEnumerator OccupiedRoutine()
    {
        _renderer.material.color = _nodeOccupiedColor;

        yield return new WaitForSeconds(_nodeOccupiedTimer);

        _renderer.material.color = _startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentSpawnable != null)
            return;

        if (other.gameObject.GetComponent<Spawnable>() != null)
            _currentSpawnable = other.gameObject.GetComponent<Spawnable>();

        other.isTrigger = false;
    }

    public void CreateNew()
    {
        Spawnable spawnableToBuild = BuildManager.Instance.GetSpawnableToBuild;

        if (spawnableToBuild == null || _playerStats.Money < Mathf.Abs(spawnableToBuild.Cost))
        {
            StartCoroutine(OccupiedRoutine());
            return;
        }

        if ((spawnableToBuild is LandMine && this is NormalNode) || (spawnableToBuild is Turret && this is LandMineNode))
            return;

        GameObject particle = Instantiate(_builtParticle, transform.position + _positionOffset, Quaternion.identity, _particleHolder);
        Destroy(particle, 5f);

        _playerStats.MoneyChanged(spawnableToBuild.Cost);

        foreach (Spawnable poolSpawnable in _spawnablePool)
            if (poolSpawnable.SpawnableType == spawnableToBuild.SpawnableType && poolSpawnable.Upgradable)
                _currentSpawnable = poolSpawnable;

        GameObject newSpawnable = _currentSpawnable.gameObject;
        newSpawnable.transform.parent = _attackedHolder;
        newSpawnable.SetActive(true);

        if(spawnableToBuild is LandMine)
            (spawnableToBuild as LandMine).SetLandMineNode(this);
    }

    public void SellCurrent()
    {
        _playerStats.MoneyChanged(Mathf.Abs(_currentSpawnable.GetComponent<Spawnable>().SellCost));

        GameObject particle = Instantiate(_sellParticle, _currentSpawnable.transform.position + _positionOffset, Quaternion.identity, _particleHolder);
        Destroy(particle, 5f);

        _currentSpawnable.gameObject.SetActive(false);
        _currentSpawnable = null;
    }

    public void UpgradeCurrent()
    {
        if (_currentSpawnable == null || !_currentSpawnable.Upgradable)
            return;

        if (Mathf.Abs(_currentSpawnable.UpgradePrice) > _playerStats.Money)
        {
            NodeOccupied();
            return;
        }
        
        _currentSpawnable.gameObject.SetActive(false);

        _playerStats.MoneyChanged(_currentSpawnable.UpgradePrice);

        foreach (Spawnable spawnable in _spawnablePool)
            if (spawnable.SpawnableType == _currentSpawnable.UpgradedSpawnablePrefab.GetComponent<Spawnable>().SpawnableType)
                _currentSpawnable = spawnable;

        GameObject upgradedSpawnable = _currentSpawnable.gameObject;
        upgradedSpawnable.transform.parent = _attackedHolder;
        upgradedSpawnable.SetActive(true);

        if (_currentSpawnable is LandMine)
            (_currentSpawnable as LandMine).SetLandMineNode(this);
    }
}