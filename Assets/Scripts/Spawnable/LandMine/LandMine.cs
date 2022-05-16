using System.Collections;
using UnityEngine;

public class LandMine : Spawnable
{
    [Header("Stats")]
    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private float _explosionRange;
    public float ExplosionRange => _explosionRange;

    private void Awake()
    {
        StartCoroutine(ShaderRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }

    private IEnumerator ShaderRoutine()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        float time = 0f;

        while (true)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                int index = 0;

                foreach (Material material in meshRenderer.materials)
                {
                    material.SetFloat("_Cutoff", 1f - (time * _disolveSpeed));

                    time += Time.deltaTime;

                    meshRenderer.materials[index] = material;

                    index++;
                }

                if (meshRenderer == meshRenderers[meshRenderers.Length - 1] && meshRenderer.materials[meshRenderer.materials.Length - 1].GetFloat("_Cutoff") == 1f)
                    yield break;
            }

            yield return null;
        }
    }
}