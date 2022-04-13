using UnityEngine;

public class LaserBeamer : Turret
{
    [SerializeField] private float _slowRate;
    public float SlowRate => _slowRate;
}