using UnityEngine;

public class GaolTrigger : MonoBehaviour
{
    #region Variables

    [SerializeField] Character player;

    BoxCollider collider;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            collider.enabled = false;
            player.Goal();
            other.GetComponent<Ball>().ResetBall();
            Invoke("ActiveCollider", 1.5f);
        }
    }

    #endregion

    #region Other Methods

    void ActiveCollider()
    {
        collider.enabled = true;
    }

    #endregion
}
