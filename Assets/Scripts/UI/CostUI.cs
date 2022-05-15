using UnityEngine;
using TMPro;

public class CostUI : MonoBehaviour
{
    [Header("Turret Prefabs")]
    [SerializeField] private Turret _standartTurret = null;
    [SerializeField] private Turret _missileLauncher = null;
    [SerializeField] private Turret _laserBeamer = null;

    [Header("Cost Texts")]
    [SerializeField] private TextMeshProUGUI _standartTurretText = null;
    [SerializeField] private TextMeshProUGUI _missileLauncherText = null;
    [SerializeField] private TextMeshProUGUI _laserBeamText = null;

    private void Awake()
    {
        _standartTurretText.text = "$" + Mathf.Abs(_standartTurret.Cost);

        _missileLauncherText.text = "$" + Mathf.Abs(_missileLauncher.Cost);

        _laserBeamText.text = "$" + Mathf.Abs(_laserBeamer.Cost);
    }
}