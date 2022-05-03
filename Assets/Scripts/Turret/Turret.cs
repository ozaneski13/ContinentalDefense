using UnityEngine;
public class Turret : MonoBehaviour, ITurret
{
    [Header("Stats")]
    [SerializeField] private ETurret _turretType;
    public ETurret TurretType => _turretType;

    [SerializeField] private int _cost;
    public int Cost => _cost;

    [SerializeField] private int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] private int _cannonCount;
    public int CannonCount => _cannonCount;

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _rotationSpeed = 10f;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _timeToCheckTarget = 0.5f;
    public float TimeToCheckTarget => _timeToCheckTarget;

    [SerializeField] private bool _upgradable = false;
    public bool Upgradable => _upgradable;

    [SerializeField] private GameObject _upgradedTurretPrefab = null;
    public GameObject UpgradedTurretPrefab => _upgradedTurretPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}