using UnityEngine;

public class AttackerTurret : Turret
{
    [SerializeField] private float _fireRate;
    public float FireRate => _fireRate;
}