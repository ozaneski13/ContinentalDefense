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

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _rotationSpeed = 10f;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _timeToCheckTarget = 0.5f;
    public float TimeToCheckTarget => _timeToCheckTarget;

    [SerializeField] private bool _upgradable = false;
    public bool Upgradable => _upgradable;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}