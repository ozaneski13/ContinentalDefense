using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    private bool _isAlreadyPressed = false;

    public void ReturnToMenu()
    {
        if (_isAlreadyPressed)
            return;

        _isAlreadyPressed = true;

        FadeUI.Instance.FadeTo(0);
    }
}