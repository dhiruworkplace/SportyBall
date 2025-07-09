using UnityEngine;
using Cinemachine;
using DG.Tweening;
using TMPro;

public class PlayerControl : Singleton<PlayerControl>
{
    #region Variables

    [Header("Objects")]
    [SerializeField] Animator playerAnim;
    [SerializeField] ParticleSystem confeti;
    [SerializeField] CinemachineVirtualCamera cm;
    [SerializeField] TextMeshProUGUI plusText;    

    [Header("Variables")]
    [SerializeField] float playerSpeed = 10f;
    int perfectCounter;

    #endregion

    #region MonoBehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
    }

    #endregion

    #region Other Methods

    // PLUS +1 SYSTEM AMAZING PERFECT
    public void PlusSpawner()
    {
        perfectCounter++;
        // PLUS Text Spawn
        plusText.gameObject.SetActive(true);
        plusText.rectTransform.DOScale(2, .3f).SetLoops(1, LoopType.Yoyo).OnComplete(() =>
        {
            plusText.rectTransform.DOScale(1, .3f);
            plusText.gameObject.SetActive(false);
        });
        plusText.rectTransform.DOMoveY(20, .3f).SetRelative();

        // Perfect & Amaze Text
        if (perfectCounter < 4 && perfectCounter != 0)
        {
            if (perfectCounter % 2 == 0)
            {
                GameManager.Instance.Amazer();
            }
            else if (perfectCounter % 3 == 0)
            {
                GameManager.Instance.Perfector();
            }
        }
        else
        {
            perfectCounter = 1;
        }
    }

    #endregion

    #region COLLISION

    // COLLIDE ENEMY
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);               
            GameManager.Instance.GameOver();
            Debug.Log("Game Over");
        } 
    }

    // TRIGGER
    private void OnTriggerEnter(Collider other)
    {
        // COIN & PLAYER 
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoin(1);
            Destroy(other.gameObject);
        }

        // FINISH & PLAYER
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.FinishLevel();
            confeti.Play();          
        }
    }

    #endregion
}