using UnityEngine;

public class LevelSettingsHolder : MonoBehaviour
{
    private static LevelSettingsHolder _instance;
    public static LevelSettingsHolder Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LevelSettingsHolder>();

            return _instance;
        }
    }

    private static bool _isLoaded = false;

    private void Awake()
    {
        if (!_isLoaded)
        {
            _isLoaded = true;
            DontDestroyOnLoad(this);
        }
    }

    [SerializeField] private LevelSettingsSO _levelSettingsSO = null;
    public LevelSettingsSO LevelSettingsSO => _levelSettingsSO;
}