using UnityEngine;

public class WaypointController : MonoBehaviour
{
    #region Singleton
    public static WaypointController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [Header("Way Points")]
    [SerializeField] private WayPoints[] _wayPointsArray;
    public WayPoints[] WayPointsArray => _wayPointsArray;
}