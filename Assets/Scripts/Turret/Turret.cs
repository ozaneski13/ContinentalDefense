using UnityEngine;
public class Turret : MonoBehaviour, ITurret
{
    [SerializeField] private int _buyPrice;
    public int BuyPrice => _buyPrice;

    [SerializeField] private int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _fireRate;
    public float FireRate => _fireRate;

    [SerializeField] private float _timeToCheckTarget;
    public float TimeToCheckTarget => _timeToCheckTarget;

    [SerializeField] private bool _upgradable = false;
    public bool Upgradable => _upgradable;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}