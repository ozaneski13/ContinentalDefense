using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleStatus : MonoBehaviour
{
    [SerializeField] private Text _toggleText = null;

    private int _counter = 0;

    public Action<EToggle> ToggleChanged;

    public void ChangeToggle()
    {
        EToggle toggleStatus = EToggle.Buy;

        switch (_counter)
        {
            case 0:
                break;

            case 1:
                toggleStatus = EToggle.Sell;
                break;

            case 2:
                toggleStatus = EToggle.Upgrade;
                _counter = -1;
                break;
        }

        _toggleText.text = toggleStatus.ToString();
        _counter++;

        ToggleChanged?.Invoke(toggleStatus);
    }
}