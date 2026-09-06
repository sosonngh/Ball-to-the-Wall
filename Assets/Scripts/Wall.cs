using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum wallType
    {
        xAxis,
        yAxis
    }

    [SerializeField] private wallType type;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Ball")) return;

        Ball ball = collision.gameObject.GetComponent<Ball>();

        switch (type)
        {
            case wallType.xAxis:
                ball.BounceOffWallX();
                break;

            case wallType.yAxis:
                ball.BounceOffWallY();
                break;

            default:
                break;
        }
    }
}
