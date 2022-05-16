using UnityEngine;

public class SpawnableHolder : MonoBehaviour
{
    #region Singleton
    public static SpawnableHolder Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}