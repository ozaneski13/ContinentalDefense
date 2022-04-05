using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Color _nodeOccupiedColor = Color.red;

    private Color _startColor = Color.white;

    [SerializeField] private Vector3 _positionOffset = Vector3.zero;

    private GameObject _currentTurrent = null;
    public GameObject CurrentTurret => _currentTurrent;

    private void Start()
    {
        _startColor = _renderer.material.color;
    }

    public void CreateNewTurret()
    {
        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild;

        _currentTurrent = Instantiate(turretToBuild, transform.position + _positionOffset, transform.rotation);
    }
}