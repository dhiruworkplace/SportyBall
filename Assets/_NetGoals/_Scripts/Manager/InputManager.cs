using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    #region Variables

    [SerializeField] Transform player;
    [SerializeField] Vector2 zLimits;
    [SerializeField] float smoothness;

    Character playerScript;

    float firstPos;
    float lastPos;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        playerScript = player.GetComponent<Character>();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted && !GameManager.Instance.isGameEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPos = Input.mousePosition.x;
                playerScript.anim.SetBool("Moving", true);
            }

            if (Input.GetMouseButton(0))
            {
                lastPos = Input.mousePosition.x;

                float distance = (lastPos - firstPos) / smoothness;
                playerScript.anim.SetTrigger("Move");
                player.Translate(player.transform.forward * distance * Time.deltaTime);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Mathf.Clamp(player.transform.position.z, zLimits.y, zLimits.x));

                firstPos = lastPos;
            }
            else
            {
                playerScript.anim.SetBool("Moving", false);
                playerScript.anim.SetTrigger("Stop");
            }
        }
    }

    #endregion
}
