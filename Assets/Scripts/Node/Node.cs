using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject _builtParticle = null;

    [Header("Node")]
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Color _nodeOccupiedColor = Color.red;
    [SerializeField] private float _nodeOccupiedTimer = 0.5f;

    private Color _startColor = Color.white;

    [Header("Positioning")]
    [SerializeField] private Vector3 _positionOffset = Vector3.zero;

    private GameObject _currentTurret = null;
    public GameObject CurrentTurret => _currentTurret;

    private PlayerStats _playerStats = null;

    private Transform _turretHolder = null;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        _turretHolder = TurretHolder.Instance.transform;

        _startColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentTurret != null)
            return;

        if (other.gameObject.GetComponent<Turret>() != null)
            _currentTurret = other.gameObject;

        other.isTrigger = false;
    }

    public void CreateNewTurret()
    {
        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild;

        if (turretToBuild == null)
            return;

        Turret turret = turretToBuild.GetComponent<Turret>();

        if (turretToBuild == null || _playerStats.Money < Mathf.Abs(turret.Cost))
        {
            StartCoroutine(OccupiedRoutine());
            return;
        }

        GameObject particle = Instantiate(_builtParticle, transform.position + _positionOffset, Quaternion.identity);
        Destroy(particle, 5f);

        _playerStats.MoneyChanged(turret.Cost);
        _currentTurret = Instantiate(turretToBuild, transform.position + _positionOffset, transform.rotation, _turretHolder);
    }

    public void SellCurrentTurret()
    {
        _playerStats.MoneyChanged(Mathf.Abs(_currentTurret.GetComponent<Turret>().Cost));

        Destroy(_currentTurret);
    }

    public void UpgradeCurrentTurret()
    {
        Turret turret = _currentTurret.GetComponent<Turret>();

        if (turret == null)
            return;

        if (!turret.Upgradable)
            return;

        if (turret.UpgradePrice > _playerStats.Money)
        {
            NodeOccupied();
            return;
        }

        //Upgrade
    }

    public void NodeOccupied()
    {
        StartCoroutine(OccupiedRoutine());
    }

    private IEnumerator OccupiedRoutine()
    {
        _renderer.material.color = _nodeOccupiedColor;

        yield return new WaitForSeconds(_nodeOccupiedTimer);

        _renderer.material.color = _startColor;
    }
}