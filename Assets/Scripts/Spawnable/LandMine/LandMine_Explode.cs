using System.Collections;
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

    [Header("Particles")]
    [SerializeField] private GameObject _explosionEffect = null;

    private Transform _particleHolder = null;

    private bool _isRoutineStarted = false;

    private void Start()
    {
        _particleHolder = ParticleHolder.Instance.transform;
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
        
        GameObject particle = Instantiate(_explosionEffect, transform.position, Quaternion.identity, _particleHolder);
        Destroy(particle, 5f);

        _landMine.LandMineNode.LandMineExplode();
        gameObject.SetActive(false);
    }

    private void DamageEnemy(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetHit(_landMine.Damage);
    }
}