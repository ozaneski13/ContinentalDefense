using UnityEngine;
public class Turret : MonoBehaviour, ITurret
{
    [SerializeField] private int _buyPrice;
    public int BuyPrice => _buyPrice;

    [SerializeField] private int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private bool _upgradable = false;
    public bool Upgradable => _upgradable;
}