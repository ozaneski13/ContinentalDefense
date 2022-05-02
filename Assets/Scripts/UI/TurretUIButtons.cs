using UnityEngine;
using UnityEngine.UI;

public class TurretUIButtons : MonoBehaviour
{
    [SerializeField] private TurretUI _turretUI = null;

    [SerializeField] private Text _upgradeButtonText = null;
    [SerializeField] private Text _sellButtonText = null;

    private void OnEnable()
    {
        _upgradeButtonText.text = "<b>UPGRADE</b>\n $" + Mathf.Abs(_turretUI.TargetNode.CurrentTurret.UpgradePrice).ToString();
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