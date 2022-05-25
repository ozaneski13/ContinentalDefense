using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager = null;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseStandartTurret()
    {
        _buildManager.SetSpawnableToBuild(_buildManager.StandartTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        _buildManager.SetSpawnableToBuild(_buildManager.MissileLauncherPrefab);
    }

    public void PurchaseLaserBeam()
    {
        _buildManager.SetSpawnableToBuild(_buildManager.LaserBeamPrefab);
    }

    public void PurchaseLandMine()
    {
        _buildManager.SetSpawnableToBuild(_buildManager.LandMinePrefab);
    }
}