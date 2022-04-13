using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [Header("Turret Prefabs")]
    [SerializeField] private Turret _standartTurret = null;
    [SerializeField] private Turret _missileLauncher = null;
    [SerializeField] private Turret _laserBeamer = null;

    [Header("Cost Texts")]
    [SerializeField] private Text _standartTurretText = null;
    [SerializeField] private Text _missileLauncherText = null;
    [SerializeField] private Text _laserBeamText = null;

    private void Awake()
    {
        _standartTurretText.text = "$" + Mathf.Abs(_standartTurret.Cost);

        _missileLauncherText.text = "$" + Mathf.Abs(_missileLauncher.Cost);

        _laserBeamText.text = "$" + Mathf.Abs(_laserBeamer.Cost);
    }
}