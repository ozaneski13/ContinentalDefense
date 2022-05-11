using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button[] _levelButtons = null;

    private bool _isAlreadyPressed = false;

    private void Start()
    {
        CheckButtons();
    }

    private void CheckButtons()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        if (levelReached == 0)
        {
            levelReached = 1;
            PlayerPrefs.SetInt("levelReached", levelReached);
        }

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                _levelButtons[i].interactable = false;

                Color disableColor = _levelButtons[i].image.color;
                disableColor.a = 40;

                _levelButtons[i].image.color = disableColor;
            }
        }
    }

    public void Select(string levelName)
    {
        if (_isAlreadyPressed)
            return;

        _isAlreadyPressed = true;

        FadeUI.Instance.FadeTo(levelName);
    }
}