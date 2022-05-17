using UnityEngine;
using TMPro;

public class CostUI : MonoBehaviour
{
    [Header("Turret Prefabs")]
    [SerializeField] private Spawnable _standartTurret = null;
    [SerializeField] private Spawnable _missileLauncher = null;
    [SerializeField] private Spawnable _laserBeamer = null;
    [SerializeField] private Spawnable _landMine = null;

    [Header("Cost Texts")]
    [SerializeField] private TextMeshProUGUI _standartTurretText = null;
    [SerializeField] private TextMeshProUGUI _missileLauncherText = null;
    [SerializeField] private TextMeshProUGUI _laserBeamText = null;
    [SerializeField] private TextMeshProUGUI _landMineText = null;

    private void Awake()
    {
        _standartTurretText.text = "$" + Mathf.Abs(_standartTurret.Cost);

        _missileLauncherText.text = "$" + Mathf.Abs(_missileLauncher.Cost);

        _laserBeamText.text = "$" + Mathf.Abs(_laserBeamer.Cost);

        _landMineText.text = "$" + Mathf.Abs(_landMine.Cost);
    }
}