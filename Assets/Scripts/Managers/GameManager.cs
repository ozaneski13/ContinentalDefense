using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerStats _playerStats = null;

    private void Start()
    {
        _playerStats = PlayerStats.Instance;

        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _playerStats.NoHealthRemain += NoHealthRemain;
    }

    private void UnregisterFromEvents()
    {
        _playerStats.NoHealthRemain -= NoHealthRemain;
    }

    private void NoHealthRemain()
    {
        EndGame();
    }

    private void EndGame()
    {
        //Show end game UI
    }

    private void PauseGame()
    {
        //Show pause menu UI
    }
}