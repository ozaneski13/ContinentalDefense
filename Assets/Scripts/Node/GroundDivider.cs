using System.Collections.Generic;
using UnityEngine;

public class GroundDivider : MonoBehaviour
{
    [Header("Land Mine Prefab")]
    [SerializeField] private GameObject _landMinePrefab = null;

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

        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3());

        for (int i = 0; i < nodeCount; i++)
        {
            GameObject subPart = new GameObject();

            currentPoint = startingPoint + (edgeLength * i);
            
            subPart.transform.position = new Vector3(transform.position.x, transform.position.y, currentPoint);
            subPart.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, edgeLength);
            subPart.tag = "Node";

            LandMineNode landMineNode = subPart.AddComponent<LandMineNode>();

            GameObject landMine = Instantiate(_landMinePrefab, subPart.transform.position, Quaternion.identity);
            landMine.GetComponent<LandMine>().SetLandMineNode(landMineNode);
            landMine.transform.position += landMineNode.PositionOffset;
            landMine.transform.parent = subPart.transform;
            landMine.SetActive(false);

            landMineNode.BuiltParticleSetter(_builtParticle);
            landMineNode.SellParticleSetter(_sellParticle);
            landMineNode.LandMineSetter(landMine.GetComponent<LandMine>());

            subPart.transform.parent = transform;

            _subParts.Add(subPart);
        }

        transform.rotation = Quaternion.Euler(rotation);
    }
}