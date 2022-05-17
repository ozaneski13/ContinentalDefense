using System.Collections.Generic;
using UnityEngine;

public class GroundDivider : MonoBehaviour
{
    [Header("Efffects")]
    [SerializeField] private GameObject _builtParticle = null;
    [SerializeField] private GameObject _sellParticle = null;

    [Header("Division Count")]
    [SerializeField] private int _landMineNodeDistance = 6;

    private List<GameObject> _subParts = null;

    private void Start()
    {
        DivideGround();
    }

    private void DivideGround()
    {
        _subParts = new List<GameObject>();

        int nodeCount = (int)transform.localScale.z / _landMineNodeDistance;

        float edgeLength = transform.localScale.z / nodeCount;

        float startingPoint = transform.position.z - (transform.localScale.z / 2) + (edgeLength / 2);
        float currentPoint;

        for (int i = 0; i < nodeCount; i++)
        {
            GameObject subPart = new GameObject();

            currentPoint = startingPoint + ((edgeLength) * i);

            if (transform.rotation.eulerAngles.y == 0)
            {
                subPart.transform.position = new Vector3(transform.position.x, transform.position.y, currentPoint);
                subPart.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, edgeLength);
            }

            else if (transform.rotation.eulerAngles.y == 90)
            {
                startingPoint = transform.position.x - (transform.localScale.z / 2) + (edgeLength / 2);
                currentPoint = startingPoint + ((edgeLength) * i);

                subPart.transform.position = new Vector3(currentPoint, transform.position.y, transform.position.z);
                subPart.transform.localScale = new Vector3(edgeLength, transform.localScale.y, transform.localScale.x);
            }

            LandMineNode landMineNode = subPart.AddComponent<LandMineNode>();
            landMineNode.BuiltParticleSetter(_builtParticle);
            landMineNode.SellParticleSetter(_sellParticle);

            subPart.transform.parent = transform;

            _subParts.Add(subPart);
        }
    }
}