using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [Header("Time Scale")]
    [SerializeField] private float _timeScale = 0.1f;

    private void OnEnable()
    {
        Time.timeScale = _timeScale;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}