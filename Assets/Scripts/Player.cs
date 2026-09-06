using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float posLimitX;
    [SerializeField] private float speed;

    [SerializeField] private AudioClip[] swingAudioClips;

    private AudioSource audioSource;
    private Animator animator;
    private GameManager gameManager;

    private Transform swingHitbox;
    private Coroutine swingCoroutine = null;

    private float moveDirection = 0;

    // (animation)
    private bool hasSwinged = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameManager = Persistent.Instance.GameManager;

        swingHitbox = transform.Find("SwingHitbox");
    }

    private void OnQuit()
    {
        gameManager.Quit();
    }

    private void OnMoveLeftRight(InputValue value)
    {
        moveDirection = value.Get<float>();
    }

    private void OnSwing()
    {
        if (!gameManager.isGameActive) return;

        // animation
        if (!hasSwinged)
        {
            animator.Play("swing-left");
            hasSwinged = true;
        }
        else
        {
            hasSwinged = false;
            animator.Play("swing-right");
        }


        if (swingCoroutine != null)
        {
            StopCoroutine(swingCoroutine);
        }

        swingCoroutine = StartCoroutine(SwingCoroutine());

    }

    private void OnReset()
    {
        if (gameManager.isGameActive) return;

        gameManager.Restart();
    }

    private IEnumerator SwingCoroutine()
    {
        swingHitbox.gameObject.SetActive(true);

        audioSource.clip = swingAudioClips[Random.Range(0, swingAudioClips.Length - 1)];
        audioSource.pitch = Random.Range(0.98f, 1.5f);
        audioSource.Play();

        yield return new WaitForSeconds(0.1f);

        swingHitbox.gameObject.SetActive(false);

        swingCoroutine = null;
    }

    private void Update()
    {
        if (!gameManager.isGameActive) return;

        animator.SetInteger("MoveDirection", (int)moveDirection);

        // can't go outside the frame
        Vector3 nextFramePos = transform.position + speed * Time.deltaTime * new Vector3(moveDirection, 0, 0);
        if (nextFramePos.x >= -posLimitX && nextFramePos.x <= posLimitX)
        {
            transform.position = nextFramePos;
        }

    }
}
