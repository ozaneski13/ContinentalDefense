using UnityEngine;

public class LandMine : Spawnable
{
    [Header("Damage Stats")]
    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _explosionRange;
    public float ExplosionRange => _explosionRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}