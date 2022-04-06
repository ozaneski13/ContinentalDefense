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

    [SerializeField] private GameObject _standartTurretPrefab = null;
    public GameObject StandartTurretPrefab => _standartTurretPrefab;

    /*[SerializeField] private GameObject _missileLauncherPrefab = null;
    public GameObject MissileLauncherPrefab => _missileLauncherPrefab;

    [SerializeField] private GameObject _laserBeamPrefab = null;
    public GameObject LaserBeamPrefab => _laserBeamPrefab;*/

    private GameObject _turretToBuild = null;
    public GameObject GetTurretToBuild => _turretToBuild;

    public void SetTurretToBuild(GameObject turretToBuild)
    {
        _turretToBuild = turretToBuild;
    }
}