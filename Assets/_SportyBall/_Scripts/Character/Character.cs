using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region Variables

    public GameObject nameText;
    public TextMesh scoreText;
    public GameObject keep;
    public GameObject smokeFx;
    public ParticleSystem confettiFx;
    public Animator anim;
    public int score;
    public int index;

    #endregion

    #region Other Methods

    public void Goal()
    {
        score--;
        scoreText.text = score.ToString();
        if (!gameObject.CompareTag("Player"))
        {
            confettiFx.Play();
            GameManager.Instance.Goal();
        }
        if (score == 0)
        {
            scoreText.text = "X";
            smokeFx.SetActive(true);
            GameManager.Instance.RemovePlayer(this);
            Destroy(keep, .3f);
        }
        AudioSporty.instance.PlaySound(1);
    }

    public void GameStarted()
    {
        nameText.SetActive(false);
    }

    public void Win()
    {
        anim.Play("Win");
        if (gameObject.CompareTag("Player"))
            GameManager.Instance.FinishLevel();
        else
            Destroy(gameObject.GetComponent<AI>());
    }

    #endregion
}
