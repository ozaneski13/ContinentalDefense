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

    private GameObject _turretToBuild = null;
    public GameObject GetTurretToBuild => _turretToBuild;

    [SerializeField] private GameObject _standartTurretPrefab = null;

    private void Start()
    {
        _turretToBuild = _standartTurretPrefab;
    }
}