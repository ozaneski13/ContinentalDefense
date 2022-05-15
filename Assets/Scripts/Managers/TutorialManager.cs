using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] private List<TriggerController> _triggerControllers = null;

    [Header("Tutorials")]
    [SerializeField] private List<GameObject> _tutorials = null;

    private GameObject _currentTutorial = null;

    private int _skippedCount = 0;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SkipCurrentTutorial();
            }
        }
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        foreach(TriggerController triggerController in _triggerControllers)
        {
            triggerController.triggerActivated += TriggerActivated;
        }
    }

    private void UnregisterFromEvents()
    {
        foreach (TriggerController triggerController in _triggerControllers)
        {
            triggerController.triggerActivated -= TriggerActivated;
        }
    }

    private void TriggerActivated(int index)
    {
        _currentTutorial = _tutorials[index];
        _currentTutorial.SetActive(true);

        Time.timeScale = 0f;
    }

    private void SkipCurrentTutorial()
    {
        if (_currentTutorial == null)
            return;

        _currentTutorial.SetActive(false);
        _currentTutorial = null;

        Time.timeScale = 1f;

        _skippedCount++;

        if (_skippedCount == _tutorials.Count)
            PlayerPrefs.SetInt("tutorialStatus", 1);
    }
}