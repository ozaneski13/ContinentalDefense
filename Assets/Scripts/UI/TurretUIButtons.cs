using UnityEngine;
using TMPro;

public class TurretUIButtons : MonoBehaviour
{
    [SerializeField] private TurretUI _turretUI = null;

    [SerializeField] private TextMeshProUGUI _upgradeButtonText = null;
    [SerializeField] private TextMeshProUGUI _sellButtonText = null;

    private void OnEnable()
    {
        if(_turretUI.TargetNode.CurrentSpawnable.Upgradable)
            _upgradeButtonText.text = "<b>UPGRADE</b>\n $" + Mathf.Abs(_turretUI.TargetNode.CurrentSpawnable.UpgradePrice).ToString();
        else
            _upgradeButtonText.text = "<b>MAX UPGRADE</b>";

        _sellButtonText.text = "<b>SELL</b>\n $" + Mathf.Abs(_turretUI.TargetNode.CurrentSpawnable.SellCost).ToString();
    }

    public void UpgradeClicked()
    {
        Node node = _turretUI.TargetNode;
        Spawnable currentTurret = node.CurrentSpawnable;

        if (!currentTurret.Upgradable)
        {
            node.NodeOccupied();
            return;
        }

        node.UpgradeCurrent();

        _turretUI.Hide();
    }

    public void SellClicked()
    {
        Node node = _turretUI.TargetNode;

        node.SellCurrent();

        _turretUI.Hide();
    }
}