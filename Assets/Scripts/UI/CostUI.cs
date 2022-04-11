using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [SerializeField] private Turret _StandartTurret = null;
    [SerializeField] private Turret _missileLauncher = null;
    //[SerializeField] private Turret _laserBeam = null;

    [SerializeField] private Text _standartTurretText = null;
    [SerializeField] private Text _missileLauncherText = null;
    //[SerializeField] private Text _laserBeamText = null;

    private void Awake()
    {
        _standartTurretText.text = "$" + _StandartTurret.Cost;

        _missileLauncherText.text = "$" + _missileLauncher.Cost;

        //_laserBeamText.text = "$" + _laserBeam.Cost;
    }
}