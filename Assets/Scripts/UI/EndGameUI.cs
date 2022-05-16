using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator = null;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _killedEnemiesText = null;
    [SerializeField] private TextMeshProUGUI _waveCountText = null;

    private bool _readyToFade = false;
    private bool _isAlreadyFading = false;

    private void Start()
    {
        InitTexts();
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (_readyToFade)
                    FadeToCredits();

                IterateAnimation();
            }
        }
    }

    private void InitTexts()
    {
        _killedEnemiesText.text = "NUMBER OF ENEMIES KILLED: " + PlayerPrefs.GetInt("killCount").ToString();
        _waveCountText.text = "NUMBER OF WAVES PASSED: " + PlayerPrefs.GetInt("finishedWaves").ToString();
    }

    private void IterateAnimation()
    {
        if (_animator.speed == 1)
            return;

        _animator.speed = 1;
    }

    public void PauseAnimator()
    {
        _animator.speed = 0;
    }

    public void StartFading()
    {
        _readyToFade = true;
    }

    private void FadeToCredits()
    {
        if (_isAlreadyFading)
            return;

        _isAlreadyFading = true;
        FadeUI.Instance.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }
}