using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour, INode
{
    [Header("Effects")]
    [SerializeField] protected GameObject _builtParticle = null;
    private GameObject _builtEffect = null;

    [SerializeField] protected GameObject _sellParticle = null;
    private GameObject _sellEffect = null;

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

    private TouchManager _touchManager = null;

    private List<Spawnable> _spawnablePool = null;

    protected PlayerStats _playerStats = null;

    protected Transform _spawnableHolder = null;

    protected Spawnable _currentSpawnable = null;
    public Spawnable CurrentSpawnable => _currentSpawnable;

    private void Awake()
    {
        InitPool();
    }

    protected void InitPool()
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

        _touchManager = TouchManager.Instance;

        _playerStats = PlayerStats.Instance;

        _spawnableHolder = SpawnableHolder.Instance.transform;

        _startColor = _renderer.material.color;

        InitParticles();
    }

    private void InitParticles()
    {
        _builtEffect = Instantiate(_builtParticle, transform.position + _positionOffset, Quaternion.identity, ParticleHolder.Instance.transform);
        _sellEffect = Instantiate(_sellParticle, transform.position + _positionOffset, Quaternion.identity, ParticleHolder.Instance.transform);
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

        if ((spawnableToBuild is LandMine && this is NormalNode) || (spawnableToBuild is Turret && this is LandMineNode))
            return;

        if (spawnableToBuild == null)
        {
            _touchManager.NothingSelected();
            return;
        }

        if (_playerStats.Money < Mathf.Abs(spawnableToBuild.Cost))
        {
            StartCoroutine(OccupiedRoutine());
            return;
        }

        StartCoroutine(ParticleRoutine(_builtEffect));

        _playerStats.MoneyChanged(spawnableToBuild.Cost);

        if (spawnableToBuild is LandMine)
            _currentSpawnable = (this as LandMineNode).LandMine;
        else
            foreach (Spawnable poolSpawnable in _spawnablePool)
                if (poolSpawnable.SpawnableType == spawnableToBuild.SpawnableType && poolSpawnable.Upgradable)
                    _currentSpawnable = poolSpawnable;

        GameObject newSpawnable = _currentSpawnable.gameObject;
        newSpawnable.transform.parent = _spawnableHolder;
        newSpawnable.SetActive(true);

        if(spawnableToBuild is LandMine)
            (spawnableToBuild as LandMine).SetLandMineNode(this);
    }

    public void SellCurrent()
    {
        _playerStats.MoneyChanged(Mathf.Abs(_currentSpawnable.GetComponent<Spawnable>().SellCost));

        StartCoroutine(ParticleRoutine(_sellEffect));

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
            {
                _currentSpawnable = spawnable;
                break;
            }

        GameObject upgradedSpawnable = _currentSpawnable.gameObject;
        upgradedSpawnable.transform.parent = _spawnableHolder;
        upgradedSpawnable.SetActive(true);

        if (_currentSpawnable is LandMine)
            (_currentSpawnable as LandMine).SetLandMineNode(this);
    }

    public void LandMineExplode()
    {
        _currentSpawnable = null;
    }

    private IEnumerator ParticleRoutine(GameObject particleGO)
    {
        particleGO.SetActive(true);

        List<ParticleSystem> particleSystems = particleGO.GetComponentsInChildren<ParticleSystem>().ToList();

        foreach(ParticleSystem particleSystem in particleSystems)
            particleSystem.Play();

        yield return new WaitForSeconds(5f);

        foreach (ParticleSystem particleSystem in particleSystems)
            particleSystem.Stop();
    }
}