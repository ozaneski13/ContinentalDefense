using TMPro;
using UnityEngine;

public class SpeedUpUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _text = null;

    private int index = 0;

    public void SpeedToggle()
    {
        if (index + 1 == 4)
            index = 0;
        else
            index++;

        if (index == 0)
        {
            _text.text = "x1";
            Time.timeScale = 1f;
        }

        else if(index == 1)
        {
            _text.text = "x2";
            Time.timeScale = 2f;
        }

        else if (index == 2)
        {
            _text.text = "x0";
            Time.timeScale = 0f;
        }

        else if( index == 3)
        {
            _text.text = "x0.5";
            Time.timeScale = 0.5f;
        }
    }
}