using UnityEngine;

public class ParticleHolder : MonoBehaviour
{
    #region Singleton
    public static ParticleHolder Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion
}