using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    #region Singleton
    public static EnemyHolder Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}