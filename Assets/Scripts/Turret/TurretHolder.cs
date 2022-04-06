using UnityEngine;

public class TurretHolder : MonoBehaviour
{
    #region Singleton
    public static TurretHolder Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}