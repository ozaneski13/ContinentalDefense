using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [Header("Way Points List")]
    [SerializeField] private List<Transform> _wayPointsList;
    public List<Transform> WayPointsList => _wayPointsList;
}