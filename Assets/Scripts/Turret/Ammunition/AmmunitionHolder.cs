using UnityEngine;

public class AmmunitionHolder : MonoBehaviour
{
    #region Singleton
    public static AmmunitionHolder Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}