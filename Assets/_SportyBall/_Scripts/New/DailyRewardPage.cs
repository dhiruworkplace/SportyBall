using System;
using UnityEngine;

public class DailyRewardPage : MonoBehaviour
{
    public DayPanel[] dayPanels;
    public Sprite[] dayImges;

    // Start is called before the first frame update
    void OnEnable()
    {
        SetData();
        CheckForReward();
    }

    private void SetData()
    {
        for (int i = 0; i < dayPanels.Length; i++)
        {
            dayPanels[i].rewardAmount = (i + 1) * 1000;
            dayPanels[i].dayText.text = "Day " + (i + 1).ToString("00");
            dayPanels[i].img.sprite = dayImges[i];
            //dayPanels[i].img.SetNativeSize();
            dayPanels[i].rewardText.text = ((i + 1) * 1000).ToString();
        }
    }

    private void CheckForReward()
    {
        if (!PlayerPrefs.HasKey("rewardTime"))
        {
            dayPanels[0].collectBtn.SetActive(true);
            dayPanels[0].collectedBtn.SetActive(false);
            dayPanels[0].lockBtn.SetActive(false);
        }
        else
        {
            int inx = PlayerPrefs.GetInt("dayValue", 0);
            for (int i = 0; i < inx; i++)
            {
                dayPanels[i].collectBtn.SetActive(false);
                dayPanels[i].collectedBtn.SetActive(true);
                dayPanels[i].lockBtn.SetActive(false);
            }

            string stime = PlayerPrefs.GetString("rewardTime");
            DateTime prevTime = DateTime.Parse(stime);
            TimeSpan ts = DateTime.Now - prevTime;

            if (ts.TotalHours >= 24 && ts.TotalHours < 48)
            {
                dayPanels[inx].collectBtn.SetActive(true);
                dayPanels[inx].collectedBtn.SetActive(false);
                dayPanels[inx].lockBtn.SetActive(false);
            }
            else if (ts.TotalHours > 48)
            {
                PlayerPrefs.DeleteKey("rewardTime");
                PlayerPrefs.DeleteKey("dayValue");
                PlayerPrefs.Save();
                ResetAll();
                CheckForReward();
            }
        }
    }

    private void ResetAll()
    {
        for (int i = 0; i < dayPanels.Length; i++)
        {
            dayPanels[i].collectBtn.SetActive(false);
            dayPanels[i].collectedBtn.SetActive(false);
            dayPanels[i].lockBtn.SetActive(true);
        }
    }

    public void Claim(int inx)
    {
        DayPanel dayPanel = dayPanels[inx];
        if (!dayPanel.lockBtn.activeSelf && !dayPanel.collectedBtn.activeSelf)
        {
            dayPanel.collectBtn.SetActive(false);
            dayPanel.collectedBtn.SetActive(true);

            AppSporty.coins += 1000 * (inx + 1);

            PlayerPrefs.SetString("rewardTime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("dayValue", (inx + 1));
            PlayerPrefs.Save();

            FindAnyObjectByType<HomeSporty>().ClaimBtn();
        }
    }
}