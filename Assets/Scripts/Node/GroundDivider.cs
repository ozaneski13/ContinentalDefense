using System.Collections.Generic;
using UnityEngine;

public class GroundDivider : MonoBehaviour
{
    [Header("Division Count")]
    [SerializeField] private int _divisionCount = 0;

    private List<GameObject> _subParts = null;

    private void Start()
    {
        DivideGround();
    }

    private void DivideGround()
    {
        _subParts = new List<GameObject>();

        float longestAxis = FindLongestAxis();

        float edgeLength = longestAxis / _divisionCount;

        for (float i = Mathf.Ceil(-_divisionCount / 2f); i <= Mathf.Floor(_divisionCount / 2f); i++)
        {
            GameObject subPart = new GameObject();

            float x = 0, y = 0, z = 0;
            float scale = 0;

            Vector3 scaleVector = new Vector3();
            scale = longestAxis / _divisionCount;

            if (longestAxis == transform.localScale.x)
            {
                x = transform.position.x + scale * i;
                y = transform.position.y;
                z = transform.position.z;

                scaleVector = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            }

            else if (longestAxis == transform.localScale.y)
            {
                x = transform.position.x;
                y = transform.position.y + scale * i;
                z = transform.position.z;

                scaleVector = new Vector3(transform.localScale.x, scale, transform.localScale.z);
            }

            else if (longestAxis == transform.localScale.z)
            {
                x = transform.position.x;
                y = transform.position.y;
                z = transform.position.z + scale * i;

                scaleVector = new Vector3(transform.localScale.x, transform.localScale.y, scale);
            }

            Vector3 position = new Vector3(x, y, z);

            subPart.transform.position = position;
            subPart.transform.rotation = Quaternion.identity;
            subPart.transform.localScale = scaleVector;
            subPart.AddComponent<BoxCollider>();
            subPart.AddComponent<LandMineNode>();

            //subPart.transform.parent = transform;

            _subParts.Add(subPart);
        }
    }

    private float FindLongestAxis()
    {
        float longestAxis = transform.localScale.x;

        if (transform.localScale.y > longestAxis)
            longestAxis = transform.localScale.y;
        else if (transform.localScale.z > longestAxis)
            longestAxis = transform.localScale.z;

        return longestAxis;
    }
}