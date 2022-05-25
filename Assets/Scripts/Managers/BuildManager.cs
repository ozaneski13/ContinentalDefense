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
    [SerializeField] private Spawnable _standartTurretPrefab = null;
    public Spawnable StandartTurretPrefab => _standartTurretPrefab;

    [SerializeField] private Spawnable _missileLauncherPrefab = null;
    public Spawnable MissileLauncherPrefab => _missileLauncherPrefab;

    [SerializeField] private Spawnable _laserBeamPrefab = null;
    public Spawnable LaserBeamPrefab => _laserBeamPrefab;

    [SerializeField] private Spawnable _landMinePrefab = null;
    public Spawnable LandMinePrefab => _landMinePrefab;

    private Spawnable _objectToBuild = null;
    public Spawnable GetSpawnableToBuild => _objectToBuild;

    private Node _selectedNode = null;

    public void SetSpawnableToBuild(Spawnable objectToBuild)
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