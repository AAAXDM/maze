using UnityEngine;

public class Pause : MonoBehaviour
{
    GameObject _resumeButton;
    BackGroundPannel _panel;
    void Awake()
    {
        _resumeButton = FindObjectOfType<ResumeButton>().gameObject;
        _panel = FindObjectOfType<BackGroundPannel>();
        _resumeButton.SetActive(false);
        _panel.gameObject.SetActive(false);
    }
    public void Paused()
    {
        Time.timeScale = 0;
        _resumeButton.SetActive(true);
        _panel.gameObject.SetActive(true);
        _panel.ShowPanel();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _resumeButton.SetActive(false);
        _panel.gameObject.SetActive(false);
    }
}
