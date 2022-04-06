using UnityEngine;

public class Ammunition : MonoBehaviour, IAmmunition
{
    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private float _slowRate;
    public float SlowRate => _slowRate;

    [SerializeField] private GameObject _bulletImpactParticle = null;

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
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Hit()
    {
        GameObject particle = Instantiate(_bulletImpactParticle, transform.position, transform.rotation, _particleHolder);
        Destroy(particle, 2f);

        Destroy(gameObject);
    }
}