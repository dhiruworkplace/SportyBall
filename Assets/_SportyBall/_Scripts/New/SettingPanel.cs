using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [Space(5)]
    public GameObject musicOn, musicOff;
    public GameObject soundOn, soundOff;

    private void OnEnable()
    {
        soundOn.SetActive(AppSporty.sound.Equals(1));
        musicOn.SetActive(AppSporty.music.Equals(1));
        soundOff.SetActive(AppSporty.sound.Equals(0));
        musicOff.SetActive(AppSporty.music.Equals(0));
    }

    public void SetMusic()
    {
        if (AppSporty.music.Equals(1))
        {
            AppSporty.music = 0;
            AudioSporty.instance.PauseMusic();
        }
        else
        {
            AppSporty.music = 1;
            AudioSporty.instance.PlayMusic();
        }
        musicOn.SetActive(AppSporty.music.Equals(1));
        musicOff.SetActive(AppSporty.music.Equals(0));
        AudioSporty.instance.PlaySound(0);
    }

    public void SetSound()
    {
        if (AppSporty.sound.Equals(1))
        {
            AppSporty.sound = 0;
        }
        else
        {
            AppSporty.sound = 1;
        }
        soundOn.SetActive(AppSporty.sound.Equals(1));
        soundOff.SetActive(AppSporty.sound.Equals(0));
        AudioSporty.instance.PlaySound(0);
    }
}