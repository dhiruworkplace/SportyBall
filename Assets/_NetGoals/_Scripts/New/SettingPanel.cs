using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [Space(5)]
    public GameObject musicOn, musicOff;
    public GameObject soundOn, soundOff;

    private void OnEnable()
    {
        soundOn.SetActive(AppGoals.sound.Equals(1));
        musicOn.SetActive(AppGoals.music.Equals(1));
        soundOff.SetActive(AppGoals.sound.Equals(0));
        musicOff.SetActive(AppGoals.music.Equals(0));
    }

    private void OnDisable()
    {
        FindAnyObjectByType<HomeGoals>().SetButtons();
    }

    public void SetMusic()
    {
        if (AppGoals.music.Equals(1))
        {
            AppGoals.music = 0;
            AudioGoals.instance.PauseMusic();
        }
        else
        {
            AppGoals.music = 1;
            AudioGoals.instance.PlayMusic();
        }
        musicOn.SetActive(AppGoals.music.Equals(1));
        musicOff.SetActive(AppGoals.music.Equals(0));
        AudioGoals.instance.PlaySound(0);
    }

    public void SetSound()
    {
        if (AppGoals.sound.Equals(1))
        {
            AppGoals.sound = 0;
        }
        else
        {
            AppGoals.sound = 1;
        }
        soundOn.SetActive(AppGoals.sound.Equals(1));
        soundOff.SetActive(AppGoals.sound.Equals(0));
        AudioGoals.instance.PlaySound(0);
    }
}