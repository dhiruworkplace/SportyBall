using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        AudioGoals.instance.PlaySound(0);
        Time.timeScale = 0f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
        AudioGoals.instance.PlaySound(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
        AudioGoals.instance.PlaySound(0);
    }

    public void Continue()
    {
        AudioGoals.instance.PlaySound(0);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}