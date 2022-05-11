using UnityEngine;

public class LevelController : MonoBehaviour
{
    public void Select(string levelName)
    {
        FadeUI.Instance.FadeTo(levelName);
    }
}