using System.Collections;
using UnityEngine;
public class Turret : MonoBehaviour, ITurret
{
    [Header("Stats")]
    [SerializeField] private ETurret _turretType;
    public ETurret TurretType => _turretType;

    [SerializeField] private int _cost;
    public int Cost => _cost;

    [SerializeField] private int _upgradePrice;
    public int UpgradePrice => _upgradePrice;

    [SerializeField] private int _cannonCount;
    public int CannonCount => _cannonCount;

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private float _rotationSpeed = 10f;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private float _timeToCheckTarget = 0.5f;
    public float TimeToCheckTarget => _timeToCheckTarget;

    [SerializeField] private bool _upgradable = false;
    public bool Upgradable => _upgradable;

    [SerializeField] private GameObject _upgradedTurretPrefab = null;
    public GameObject UpgradedTurretPrefab => _upgradedTurretPrefab;

    [SerializeField] private float _disolveSpeed = 0.25f;

    private void Awake()
    {
        StartCoroutine(ShaderRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private IEnumerator ShaderRoutine()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        float time = 0f;

        while (true)
        {
            foreach(MeshRenderer meshRenderer in meshRenderers)
            {
                int index = 0;

                foreach(Material material in meshRenderer.materials)
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