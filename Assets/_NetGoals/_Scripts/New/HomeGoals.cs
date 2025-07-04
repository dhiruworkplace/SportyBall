using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeGoals : MonoBehaviour
{
    public TextMeshProUGUI questTimer;
    public TextMeshProUGUI questTimer1;

    public GameObject musicOff;
    public GameObject soundOff;

    public GameObject openBtn;
    public GameObject openBox, closeBox;

    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CheckQuest), 0f, 1f);
        SetButtons();
        SetCoin();
    }

    public void SetCoin()
    {
        coinText.text = AppGoals.coins.ToString();
    }

    public void SetButtons()
    {
        soundOff.SetActive(AppGoals.sound.Equals(0));
        musicOff.SetActive(AppGoals.music.Equals(0));        
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(CheckQuest));
        openBtn.SetActive(true);
    }

    private void CheckQuest()
    {
        DateTime lastDT = new DateTime();
        if (!PlayerPrefs.HasKey("lastSpin"))
        {
            //PlayerPrefs.SetString("lastSpin", DateTime.Now.AddHours(24).ToString());
            //PlayerPrefs.Save();

            questTimer.text = "Claim";
            StopTimer();
            return;
        }
        lastDT = DateTime.Parse(PlayerPrefs.GetString("lastSpin"));

        TimeSpan diff = (lastDT - DateTime.Now);
        questTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", diff.Hours, diff.Minutes, diff.Seconds);
        questTimer1.text = string.Format("{0:D2}:{1:D2}:{2:D2}", diff.Hours, diff.Minutes, diff.Seconds);

        if (diff.TotalSeconds <= 0)
        {
            StopTimer();
            questTimer.text = "Claim";
        }
    }

    private void StartTimer()
    {
        PlayerPrefs.SetString("lastSpin", DateTime.Now.AddHours(24).ToString());
        PlayerPrefs.Save();

        InvokeRepeating(nameof(CheckQuest), 0f, 1f);
    }

    public void ClaimBtn()
    {
        if (questTimer.text.Equals("Claim"))
        {
            AppGoals.coins += 100;
            SetCoin();
            openBtn.SetActive(false);

            openBox.SetActive(true);
            closeBox.SetActive(false);

            Invoke(nameof(StartTimer), 0f);
        }
        Click();
    }

    public void Click()
    {
        AudioGoals.instance.PlaySound(0);
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
        soundOff.SetActive(AppGoals.sound.Equals(0));
        AudioGoals.instance.PlaySound(0);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}