using UnityEngine;

public class Turret : Spawnable, ITurret
{
    [SerializeField] private int _cannonCount;
    public int CannonCount => _cannonCount;

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _rotationSpeed = 10f;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _timeToCheckTarget = 0.5f;
    public float TimeToCheckTarget => _timeToCheckTarget;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}