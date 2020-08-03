using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float maxtime;
    private float range;

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject panel;

    public void SetDisplayTime(float time)
    {
        slider.value = slider.maxValue;
        maxtime = time;
        range = slider.maxValue - slider.minValue;
    }

    public void DisplayTime(float time)
    {
        float value = time / maxtime * range;
        slider.value = value;
    }

    public void onButtonPlayClick()
    {
        GameManager.Instance.GameStart();
        GameManager.Instance.onGameOver += onGameOver;
    }

    public void onGameOver()
    {
        status.text = "Game Over";
        panel.SetActive(true);
    }
}
