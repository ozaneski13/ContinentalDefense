using UnityEngine;
using System.Collections;

public class Spawnable : MonoBehaviour, ISpawnable
{
    [Header("Stats")]

    [SerializeField] private ESpawnable _spawnableType;
    public ESpawnable SpawnableType => _spawnableType;

    [SerializeField] protected int _cost;
    public int Cost => _cost;

    [SerializeField] protected int _sellCost;
    public int SellCost => _sellCost;

    [SerializeField] protected int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] protected bool _upgradable = false;
    public bool Upgradable => _upgradable;

    [SerializeField] protected bool _isUpgraded = false;
    public bool IsUpgraded => _isUpgraded;

    [SerializeField] protected GameObject _upgradedSpawnablePrefab = null;
    public GameObject UpgradedSpawnablePrefab => _upgradedSpawnablePrefab;

    [SerializeField] protected float _disolveSpeed = 0.25f;

    private void OnEnable()
    {
        StartCoroutine(ShaderRoutine());
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