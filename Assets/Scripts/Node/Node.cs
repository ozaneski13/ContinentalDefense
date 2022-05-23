using System.Collections;
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

    protected Color _startColor = Color.white;

    [Header("Positioning")]
    [SerializeField] protected Vector3 _positionOffset = new Vector3(0, 0.5f, 0);
    public Vector3 PositionOffset => _positionOffset;

    protected PlayerStats _playerStats = null;

    protected Transform _particleHolder = null;

    protected Transform _attackedHolder = null;

    protected Spawnable _currentSpawnable = null;
    public Spawnable CurrentSpawnable => _currentSpawnable;

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
        GameObject spawnableToBuild = BuildManager.Instance.GetSpawnableToBuild;

        if (spawnableToBuild == null)
            return;

        if ((spawnableToBuild.GetComponent<LandMine>() != null && this is NormalNode) || (spawnableToBuild.GetComponent<Turret>() != null && this is LandMineNode))
            return;

        Spawnable spawnable = spawnableToBuild.GetComponent<Spawnable>();

        if (spawnableToBuild == null || _playerStats.Money < Mathf.Abs(spawnable.Cost))
        {
            StartCoroutine(OccupiedRoutine());
            return;
        }

        GameObject particle = Instantiate(_builtParticle, transform.position + _positionOffset, Quaternion.identity, _particleHolder);
        Destroy(particle, 5f);

        _playerStats.MoneyChanged(spawnable.Cost);

        GameObject newSpawnable = Instantiate(spawnableToBuild, transform.position + _positionOffset, transform.rotation, _attackedHolder);
        _currentSpawnable = newSpawnable.GetComponent<Spawnable>();
    }

    public void SellCurrent()
    {
        _playerStats.MoneyChanged(Mathf.Abs(_currentSpawnable.GetComponent<Turret>().Cost));

        GameObject particle = Instantiate(_sellParticle, _currentSpawnable.transform.position + _positionOffset, Quaternion.identity, _particleHolder);
        Destroy(particle, 5f);

        Destroy(_currentSpawnable.gameObject);
        _currentSpawnable = null;
    }

    public void UpgradeCurrent()
    {
        Spawnable spawnable = _currentSpawnable.GetComponent<Spawnable>();

        if (spawnable == null || !spawnable.Upgradable)
            return;

        if (Mathf.Abs(spawnable.UpgradePrice) > _playerStats.Money)
        {
            NodeOccupied();
            return;
        }

        Destroy(_currentSpawnable.gameObject);

        _playerStats.MoneyChanged(spawnable.UpgradePrice);

        GameObject upgradedSpawnable = Instantiate(spawnable.UpgradedSpawnablePrefab, transform.position + _positionOffset, transform.rotation, _attackedHolder);
        _currentSpawnable = upgradedSpawnable.GetComponent<Spawnable>();
    }
}