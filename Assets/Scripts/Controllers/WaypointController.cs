using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] private WayPoints[] _wayPointsArray;
    public WayPoints[] WayPointsArray => _wayPointsArray;

    #region Singleton
    public static WaypointController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}