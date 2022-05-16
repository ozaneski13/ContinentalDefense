using UnityEngine;

public class Spawnable : MonoBehaviour, ISpawnable
{
    [Header("Stats")]
    [SerializeField] protected int _cost;
    public int Cost => _cost;

    [SerializeField] protected int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] protected bool _upgradable = false;
    public bool Upgradable => _upgradable;

    [SerializeField] protected GameObject _upgradedSpawnablePrefab = null;
    public GameObject UpgradedSpawnablePrefab => _upgradedSpawnablePrefab;

    [SerializeField] protected float _disolveSpeed = 0.25f;
}