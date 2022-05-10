using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _turrets = new List<GameObject>();

    private void Awake()
    {
        int randomTurret = Random.Range(0, _turrets.Count);

        _turrets[randomTurret].SetActive(true);
    }

    public void Play()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}