using UnityEngine;
using TMPro;

public class QualitySettingsController : MonoBehaviour
{
    [Header("Quality Setting UI")]
    [SerializeField] private TextMeshProUGUI _qualitySettingsText = null;

    private string[] _qualitySettingsNames = null;

    private int _currentIndex = 0;

    private void Awake()
    {
        _currentIndex = PlayerPrefs.GetInt("qualitySetting", 5);

        _qualitySettingsNames = QualitySettings.names;

        _qualitySettingsText.text = _qualitySettingsNames[_currentIndex];
    }

    public void ChangeQualitySetting()
    {
        QualitySettings.SetQualityLevel(_currentIndex, true);

        if (_currentIndex == _qualitySettingsNames.Length - 1)
            _currentIndex = 0;
        else
            _currentIndex++;

        _qualitySettingsText.text = _qualitySettingsNames[_currentIndex];

        PlayerPrefs.SetInt("qualitySetting", _currentIndex);
    }
}