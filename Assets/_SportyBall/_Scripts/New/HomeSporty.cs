using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSporty : MonoBehaviour
{
    public TextMeshProUGUI questTimer;
    public TextMeshProUGUI levelNo;

    public TextMeshProUGUI coinText;
    public GameObject agreementPanel;
    public static bool showAgreement = true;
    public GameObject storyPanel;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CheckQuest), 0f, 1f);
        SetCoin();

        if (showAgreement)
        {
            showAgreement = false;
            agreementPanel.SetActive(true);
        }

        levelNo.text = "Level " + AppSporty.saveLevel.ToString("00");
    }

    public void SelectLevel()
    {
        storyPanel.SetActive(true);
    }

    public void SetCoin()
    {
        coinText.text = AppSporty.coins.ToString();
    }

    private void StopTimer()
    {
        CancelInvoke(nameof(CheckQuest));
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
        SetCoin();
        Invoke(nameof(StartTimer), 0f);
        Click();
    }

    public void Click()
    {
        AudioSporty.instance.PlaySound(0);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}