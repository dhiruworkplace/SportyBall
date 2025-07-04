using DG.Tweening;
using UnityEngine;

public class AI : Character
{
    #region Variables

    [SerializeField] Vector2 moveLimits;
    [SerializeField] bool onZAxis;

    float movementSpeed;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        movementSpeed = .5f;
    }

    #endregion

    #region Other Methods

    public void Move()
    {
        float randomPoint = Random.Range(moveLimits.x, moveLimits.y);
        anim.SetBool("Moving", true);
        anim.SetTrigger("Move");
        if (onZAxis)
        {
            transform.DOMoveZ(randomPoint, movementSpeed).SetEase(Ease.Linear).OnComplete(delegate
            {
                Move();
            });
        }
        else
        {
            transform.DOMoveX(randomPoint, movementSpeed).OnComplete(delegate
            {
                Move();
            });
        }
    }

    #endregion
}
