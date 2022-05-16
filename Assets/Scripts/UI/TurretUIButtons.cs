using UnityEngine;
using TMPro;

public class TurretUIButtons : MonoBehaviour
{
    [SerializeField] private TurretUI _turretUI = null;

    [SerializeField] private TextMeshProUGUI _upgradeButtonText = null;
    [SerializeField] private TextMeshProUGUI _sellButtonText = null;

    private void OnEnable()
    {
        if(_turretUI.TargetNode.CurrentTurret.Upgradable)
            _upgradeButtonText.text = "<b>UPGRADE</b>\n $" + Mathf.Abs(_turretUI.TargetNode.CurrentTurret.UpgradePrice).ToString();
        else
            _upgradeButtonText.text = "<b>MAX UPGRADE</b>";

        _sellButtonText.text = "<b>SELL</b>\n $" + Mathf.Abs(_turretUI.TargetNode.CurrentTurret.Cost).ToString();
    }

    public void UpgradeClicked()
    {
        Node node = _turretUI.TargetNode;
        Turret currentTurret = node.CurrentTurret;

        if (!currentTurret.Upgradable)
        {
            node.NodeOccupied();
            return;
        }

        node.UpgradeCurrentTurret();

        _turretUI.Hide();
    }

    public void SellClicked()
    {
        Node node = _turretUI.TargetNode;

        node.SellCurrentTurret();

        _turretUI.Hide();
    }
}