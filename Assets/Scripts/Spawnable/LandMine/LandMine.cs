using UnityEngine;

public class LandMine : Spawnable
{
    [Header("Damage Stats")]
    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _explosionRange;
    public float ExplosionRange => _explosionRange;

    private Node _landMineNode = null;
    public Node LandMineNode => _landMineNode;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }

    public void SetLandMineNode(Node node)
    {
        _landMineNode = node;
    }
}