using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeUI : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private Image _fadeImage = null;
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private AnimationCurve _fadeCurve = null;

    #region Singleton
    public static FadeUI Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += SceneLoaded;
    }
    #endregion

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int sceneNumber)
    {
        StartCoroutine(FadeOut(sceneNumber));
    }

    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float time = 1f;

        while (time > 0f)
        {
            time -= Time.deltaTime * _fadeSpeed;
            float alpha = _fadeCurve.Evaluate(time);
            _fadeImage.color = new Color(0f, 0f, 0f, alpha);

            yield return null;
        }
    }

    private IEnumerator FadeOut(int sceneNumber)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * _fadeSpeed;
            float alpha = _fadeCurve.Evaluate(time);
            _fadeImage.color = new Color(0f, 0f, 0f, alpha);

            yield return null;
        }

        SceneManager.LoadScene(sceneNumber);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * _fadeSpeed;
            float alpha = _fadeCurve.Evaluate(time);
            _fadeImage.color = new Color(0f, 0f, 0f, alpha);

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}