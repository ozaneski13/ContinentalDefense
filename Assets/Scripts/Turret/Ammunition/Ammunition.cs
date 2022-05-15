using UnityEngine;

public class Ammunition : MonoBehaviour, IAmmunition
{
    [Header("Stats")]

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private float _explosionRadius;
    public float ExplosionRadius => _explosionRadius;

    [Header("Effects")]
    [SerializeField] private GameObject _bulletImpactParticle = null;

    [Header("Layers")]
    [SerializeField] private LayerMask _targetsLayer;

    private Transform _target = null;

    private Transform _particleHolder = null;

    private void Start()
    {
        _particleHolder = ParticleHolder.Instance.transform;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);

            return;
        }

        Vector3 direction = _target.position - transform.position;
        float deltaDistance = _speed * Time.deltaTime;

        if (direction.magnitude <= deltaDistance)
        {
            Hit();

            return;
        }

        transform.Translate(direction.normalized * deltaDistance, Space.World);
        transform.LookAt(_target);
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        transform.LookAt(_target);
    }

    private void Hit()
    {
        GameObject particle = Instantiate(_bulletImpactParticle, transform.position, transform.rotation, _particleHolder);
        Destroy(particle, 5f);

        if (_explosionRadius > 0)
            Explode();

        else
            DamageEnemy(_target.gameObject);

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] hitArray = Physics.OverlapSphere(transform.position, _explosionRadius, _targetsLayer);

        foreach (Collider collider in hitArray)
            DamageEnemy(collider.gameObject);
    }

    private void DamageEnemy(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetHit(_damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}