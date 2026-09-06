using UnityEngine;
using UnityEngine.Audio;

public class Ball : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speedPerSecond;

    [SerializeField] private AudioSource regularBounceSound;
    [SerializeField] private AudioSource pickupSound;

    private Vector2 moveDirection;
    private float currentSpeed;

    private Rigidbody2D rb;

    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // stay in place until player hits
        moveDirection = Vector2.zero;

        currentSpeed = baseSpeed;

        gameManager = Persistent.Instance.GameManager;

        gameManager.ballStartedMoving = false;
    }

    public Vector2 GetCurrentSpeed()
    {
        return rb.linearVelocity;
    }
    public void ChangeSpeed(float speedDelta)
    {
        currentSpeed += speedDelta; // use negative delta to slow, positive to accelerate
    }

    public void BounceOffWallX()
    {
        moveDirection.x = -moveDirection.x;

        regularBounceSound.pitch = Random.Range(0.9f, 1.1f);
        regularBounceSound.Play();
    }

    public void BounceOffWallY()
    {
        moveDirection.y = -moveDirection.y;

        regularBounceSound.pitch = Random.Range(0.9f, 1.1f);
        regularBounceSound.Play();
    }

    public void BounceOffPlayerHit(Vector2 newDirection)
    {
        // start moving (so that speed doesn't increase before that)
        if (!gameManager.ballStartedMoving) gameManager.ballStartedMoving = true; // idk

        moveDirection = newDirection.normalized;

        regularBounceSound.pitch = Random.Range(0.9f, 1.1f);
        regularBounceSound.Play();

    }

    public void PlayPickupSound()
    {
        pickupSound.pitch = Random.Range(0.9f, 1.2f);
        pickupSound.Play();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    private void Update()
    {
        if (!gameManager.ballStartedMoving) return;
        currentSpeed += speedPerSecond * Time.deltaTime;

        // ball's speed can never go negative
        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        // update score multiplier based on current ball speed
        gameManager.UpdateScoreMultiplier(currentSpeed, baseSpeed);
    }
}
