using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [Header("GameObjects")]
    [SerializeField] GameObject[] levelHolder;
    [SerializeField] Ball ball;
    [SerializeField] List<Character> allPlayers;
    [SerializeField] AI[] AIArray;

    [Header("UI Panel")]
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI coinText;

    [Header(("Perfect Sprites"))]
    [SerializeField] Image amazing;
    [SerializeField] Image perfect;

    [Header(("Variables"))]
    public bool isGameStarted;
    public bool isGameEnd;
    int level;

    public TextMeshProUGUI winScore;
    public TextMeshProUGUI loseScore;
    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            winScore.text = value.ToString("00");
            loseScore.text = value.ToString("00");
        }
    }

    //////////////////////////////////

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        GetDatas();
    }

    private void Start()
    {
        Score = 0;
        LevelGenerator();
    }

    public void Goal()
    {
        Score++;
    }

    void StartGame()
    {
        foreach (AI AI in AIArray)
        {
            AI.Move();
            AI.GameStarted();
        }
        allPlayers[0].GameStarted();
    }

    public void WhoWin()
    {
        if (allPlayers.Count == 1)
        {
            isGameStarted = false;
            allPlayers[0].Win();
        }
    }

    public void RemovePlayer(Character ch)
    {
        allPlayers.Remove(ch);
        if (ch.index == 0)
            GameOver();
        WhoWin();
    }

    public void FinishLevel()
    {
        if (isGameEnd)
            return;

        isGameEnd = true;
        AudioSporty.instance.PlaySound(2);
        StartCoroutine(FinishPanel());
        LevelUp();
    }

    public void GameOver()
    {
        if (isGameEnd)
            return;

        isGameEnd = true;
        AudioSporty.instance.PlaySound(3);
        StartCoroutine(OverPanel());
    }

    IEnumerator FinishPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
        AddCoin(100);
    }

    IEnumerator OverPanel()
    {
        yield return new WaitForSeconds(0f);
        gameOverPanel.SetActive(true);
    }

    private void LevelGenerator()
    {
        int i = level - 1;
        //levelHolder[i].SetActive(true);
        levelText.text = "LEVEL " + level.ToString();
    }

    void LevelUp()
    {
        level++;
        PlayerPrefs.SetInt("level", level);

        if ((AppSporty.selectedLevel + 1) > AppSporty.saveLevel && AppSporty.selectedLevel <= 15)
            AppSporty.saveLevel = (AppSporty.selectedLevel + 1);
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(0);
    }

    public void GetDatas()
    {
        // LEVEL
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            level = 1;
        }

        // SOUND
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        UpdateCoinText();
    }

    public void AddCoin(int newCoin)
    {
        AppSporty.coins += newCoin;
        UpdateCoinText();
    }

    public void StartButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        isGameStarted = true;
        StartCoroutine(ball.LoadBall(.5f));
        StartGame();
    }

    public void RestartButton()
    {
        AudioSporty.instance.PlaySound(0);
        SceneManager.LoadScene("Game");
    }

    private void UpdateCoinText()
    {
        coinText.text = AppSporty.coins.ToString();
    }

    // PERFECT SYSTEM
    public void Perfector()
    {
        perfect.gameObject.SetActive(true);
        perfect.transform.DOScale(5, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            perfect.transform.DOScale(1, 0);
            perfect.gameObject.SetActive(false);
        });
    }

    public void Amazer()
    {
        amazing.gameObject.SetActive(true);
        amazing.transform.DOScale(5, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            amazing.transform.DOScale(1, 0);
            amazing.gameObject.SetActive(false);
        });
    }

    public void Home()
    {
        AudioSporty.instance.PlaySound(0);
        SceneManager.LoadScene("Home");
    }

    public void OnNext()
    {
        Time.timeScale = 1f;
        if (AppSporty.selectedLevel < 15)
        {
            AppSporty.selectedLevel++;
            SceneManager.LoadScene("Game");
        }
        else
        {
            AppSporty.selectedLevel = 0;
            SceneManager.LoadScene("Home");
        }
        AudioSporty.instance.PlaySound(0);
    }
}