using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject _builtParticle = null;

    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Color _nodeOccupiedColor = Color.red;
    [SerializeField] private float _nodeOccupiedTimer = 0.5f;

    private Color _startColor = Color.white;

    [SerializeField] private Vector3 _positionOffset = Vector3.zero;
    
    private Transform _turretHolder = null;

    private GameObject _currentTurret = null;
    public GameObject CurrentTurret => _currentTurret;

    private PlayerStats _playerStats = null;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        _turretHolder = TurretHolder.Instance.transform;

        _startColor = _renderer.material.color;
    }

    public void CreateNewTurret()
    {
        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild;
        Turret turret = turretToBuild.GetComponent<Turret>();

        if (turretToBuild == null || _playerStats.Money < turret.Cost)
        {
            StartCoroutine(OccupiedRoutine());
            return;
        }

        GameObject particle = Instantiate(_builtParticle, transform.position + _positionOffset, Quaternion.identity);
        Destroy(particle, 5f);

        _playerStats.MoneyChanged(-turret.Cost);
        _currentTurret = Instantiate(turretToBuild, transform.position + _positionOffset, transform.rotation, _turretHolder);
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