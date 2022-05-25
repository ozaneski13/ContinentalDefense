using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameWinUI : MonoBehaviour
{
    [Header("Wave Manager")]
    [SerializeField] private WaveManager _waveManager = null;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _roundText = null;

    [Header("Time Scale")]
    [SerializeField] private float _timeScale = 0.1f;
    [SerializeField] private float _lateStartDuration = 1f;

    private bool _isButtonAlreadyPressed = false;

    private void Start()
    {
        _roundText.text = _waveManager.GetCurrentWave().ToString();

        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(_lateStartDuration);

        Time.timeScale = _timeScale;
    }

    public void NextButtonPressed()
    {
        if (_isButtonAlreadyPressed)
            return;

        _isButtonAlreadyPressed = true;

        Time.timeScale = 1f;

        FadeUI.Instance.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuButtonPressed()
    {
        if (_isButtonAlreadyPressed)
            return;

        _isButtonAlreadyPressed = true;

        Time.timeScale = 1f;

        FadeUI.Instance.FadeTo(0);
    }
}