using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _turrets = new List<GameObject>();

    private bool _isButtonAlreadyPressed = false;

    private void Awake()
    {
        int randomTurret = Random.Range(0, _turrets.Count);

        _turrets[randomTurret].SetActive(true);
    }

    public void Play()
    {
        if (_isButtonAlreadyPressed)
            return;
        
        _isButtonAlreadyPressed = true;
        FadeUI.Instance.FadeTo(1);
    }

    public void Credits()
    {
        if (_isButtonAlreadyPressed)
            return;

        _isButtonAlreadyPressed = true;
        

    }

    public void Quit()
    {
        if (_isButtonAlreadyPressed)
            return;
        
        _isButtonAlreadyPressed = true;
        Application.Quit();
    }
}