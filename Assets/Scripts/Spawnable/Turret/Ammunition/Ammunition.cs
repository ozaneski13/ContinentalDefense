using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject _ammoImpactParticle = null;
    private GameObject _ammoImpactEfffect = null;

    [Header("Layers")]
    [SerializeField] private LayerMask _targetsLayer;

    private Turret_Attack _attacker = null;

    private List<ParticleSystem> _particleSystems = null;

    private Transform _target = null;

    private bool _selfDestructActivated = false;

    private void Awake()
    {
        _ammoImpactEfffect = Instantiate(_ammoImpactParticle, transform.position, transform.rotation, ParticleHolder.Instance.transform);
        _ammoImpactEfffect.SetActive(false);

        _particleSystems = _ammoImpactEfffect.GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void OnEnable()
    {
        _selfDestructActivated = false;

        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Stop();
    }

    private void Update()
    {
        if (!_target.gameObject.activeInHierarchy || _target == null)
        {
            SelfDestruct();

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

    public void SetAttacker(Turret_Attack attacker)
    {
        _attacker = attacker;
    }

    private void Hit()
    {
        _ammoImpactEfffect.transform.position = transform.position;
        _ammoImpactEfffect.SetActive(true);

        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Play();

        if (_explosionRadius > 0)
            Explode();

        else
            DamageEnemy(_target.gameObject);

        _attacker.RefillPool(gameObject);
        gameObject.SetActive(false);
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

    private void SelfDestruct()
    {
        if (_selfDestructActivated)
            return;

        _selfDestructActivated = true;

        _attacker.RefillPool(gameObject);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}