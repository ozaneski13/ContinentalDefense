using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LandMine))]
public class LandMine_Explode : MonoBehaviour
{
    [Header("Land Mine")]
    [SerializeField] private LandMine _landMine = null;

    [Header("Explosion Options")]
    [SerializeField] private float _durationBeforeExplosion = 1f;

    [Header("Layers")]
    [SerializeField] private LayerMask _targetsLayer;

    private GameObject _explosionEffect = null;

    private List<ParticleSystem> _particleSystems = null;

    private bool _isRoutineStarted = false;

    private void Start()
    {
        if (_particleSystems == null && _explosionEffect != null)
            _particleSystems = _explosionEffect.GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void OnEnable()
    {
        if(_particleSystems != null)
            foreach (ParticleSystem particleSystem in _particleSystems)
                particleSystem.Stop();

        _isRoutineStarted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy == null)
            return;

        if(!_isRoutineStarted)
            StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        _isRoutineStarted = true;

        yield return new WaitForSeconds(_durationBeforeExplosion);

        Collider[] hitArray = Physics.OverlapSphere(transform.position, _landMine.ExplosionRange, _targetsLayer);

        foreach (Collider collider in hitArray)
            DamageEnemy(collider.gameObject);

        _explosionEffect.transform.parent = ParticleHolder.Instance.transform;
        _explosionEffect.transform.position = transform.position;
        _explosionEffect.SetActive(true);

        foreach (ParticleSystem particleSystem in _particleSystems)
            particleSystem.Play();

        _landMine.LandMineNode.LandMineExplode();
        gameObject.SetActive(false);
    }

    private void DamageEnemy(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetHit(_landMine.Damage);
    }

    public void SetEffect(GameObject explosionEffect)
    {
        _explosionEffect = explosionEffect;
    }
}