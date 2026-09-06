using TMPro;
using UnityEngine;

public class Swing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Ball ball = collision.gameObject.GetComponent<Ball>();

        Vector2 newDirection = ball.transform.position - transform.position;

        ball.BounceOffPlayerHit(newDirection);

        // add score for each successful player hit
        Persistent.Instance.GameManager.GainScore();
        Persistent.Instance.GameCanvas.DisplayScoreGainedMessage(transform.parent.position);
    }
}
