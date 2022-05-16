using UnityEngine;

public class SlowerTurret : Turret
{
    [SerializeField] private float _slowRate;
    public float SlowRate => _slowRate;
}