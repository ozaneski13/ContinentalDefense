using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _turrets = new List<GameObject>();

    private void Awake()
    {
        int randomTurret = Random.Range(0, _turrets.Count);

        _turrets[randomTurret].SetActive(true);
    }

    public void Play()
    {
        FadeUI.Instance.FadeTo(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}