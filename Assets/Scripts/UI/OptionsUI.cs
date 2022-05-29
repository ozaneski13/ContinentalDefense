using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    public void ReturnToMenu()
    {
        FadeUI.Instance.FadeTo(0);
    }
}