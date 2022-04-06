using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager = null;

    private int _money = 0;

    private void Start()
    {
        _buildManager = BuildManager.Instance;
    }

    public void PurchaseStandartTurret()
    {
        _buildManager.SetTurretToBuild(_buildManager.StandartTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        //_buildManager.SetTurretToBuild(_buildManager.MissileLauncherPrefab);
    }

    public void PurchaseLaserBeam()
    {
        //_buildManager.SetTurretToBuild(_buildManager.LaserBeamPrefab);
    }

    public void GainMoney(int gain)
    {
        _money += gain;
    }
}