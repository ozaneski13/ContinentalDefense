using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    public static BuildManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [Header("Turret UI")]
    [SerializeField] private TurretUI _turretUI = null;

    [Header("Turret Prefabs")]
    [SerializeField] private GameObject _standartTurretPrefab = null;
    public GameObject StandartTurretPrefab => _standartTurretPrefab;

    [SerializeField] private GameObject _missileLauncherPrefab = null;
    public GameObject MissileLauncherPrefab => _missileLauncherPrefab;

    [SerializeField] private GameObject _laserBeamPrefab = null;
    public GameObject LaserBeamPrefab => _laserBeamPrefab;

    private GameObject _objectToBuild = null;
    public GameObject GetSpawnableToBuild => _objectToBuild;

    private Node _selectedNode = null;

    public void SetTurretToBuild(GameObject objectToBuild)
    {
        _objectToBuild = objectToBuild;

        DeSelectNode();
    }

    public void SetNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeSelectNode();

            return;
        }

        _turretUI.Hide();

        _selectedNode = node;
        _objectToBuild = null;

        _turretUI.SetTargetNode(_selectedNode);
    }

    private void DeSelectNode()
    {
        _selectedNode = null;
        _turretUI.Hide();
    }
}