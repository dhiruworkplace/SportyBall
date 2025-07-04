using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Variables

    // Random Directions
    [SerializeField] Vector3[] directions;
    [SerializeField] float force;

    Vector3 startPos;
    Vector3 lastVelocity;
    Vector3 firstVelocity;

    bool goal;

    // Components
    Rigidbody rb;
    TrailRenderer trail;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        goal = true;
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        startPos = transform.position;
        firstVelocity = Vector3.zero;
    }

    // Ricochet
    private void OnCollisionEnter(Collision collision)
    {
        if (!goal)
        {
            Vector3 normal = collision.contacts[collision.contactCount - 1].normal;
            Vector3 direction = Vector3.Reflect(lastVelocity, normal).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            rb.velocity = direction * firstVelocity.magnitude;
            lastVelocity = rb.velocity;
        }
    }

    private void Update()
    {
        if (!goal)
            transform.Rotate(Vector3.right * 500 * Time.deltaTime);
        if (GameManager.Instance.isGameEnd)
            Destroy(gameObject);
    }

    #endregion

    #region Other Methods

    public IEnumerator LoadBall(float time)
    {
        int random = Random.Range(0, directions.Length);
        Vector3 randomForce = directions[random];
        randomForce.y = transform.position.y;
        Vector3 forceDirection = randomForce;

        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
        transform.position = startPos;      

        yield return new WaitForSeconds(time);
        trail.enabled = true;
        goal = false;
        rb.velocity = forceDirection * force;
        transform.rotation = Quaternion.LookRotation(forceDirection);
        if (firstVelocity == Vector3.zero)
            firstVelocity = rb.velocity;
        lastVelocity = rb.velocity;
    }

    public void ResetBall()
    {
        goal = true;
        trail.enabled = false;
        rb.velocity = Vector3.zero;
        StartCoroutine(LoadBall(1));
    }

    #endregion
}
