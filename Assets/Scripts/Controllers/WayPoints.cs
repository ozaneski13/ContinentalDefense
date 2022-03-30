using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPointsList;
    public List<Transform> WayPointsList => _wayPointsList;
}