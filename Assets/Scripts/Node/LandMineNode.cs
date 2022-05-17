using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class LandMineNode : Node
{
    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.mass = 0;
        rb.angularDrag = 0;

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    public void BuiltParticleSetter(GameObject builtParticle)
    {
        _builtParticle = builtParticle;
    }

    public void SellParticleSetter(GameObject sellParticle)
    {
        _sellParticle = sellParticle;
    }
}