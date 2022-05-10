using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [Header("Wave Manager")]
    [SerializeField] private WaveManager _waveManager = null;

    [Header("Texts")]
    [SerializeField] private Text _roundText = null;

    [Header("Time Scale")]
    [SerializeField] private float _timeScale = 0.1f;

    private void Start()
    {
        _roundText.text = _waveManager.GetCurrentWave().ToString();

        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.5f);

        Time.timeScale = _timeScale;
    }

    public void RetryButtonPressed()
    {
        Time.timeScale = 1f;

        FadeUI.Instance.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButtonPressed()
    {
        FadeUI.Instance.FadeTo(0);
    }
}