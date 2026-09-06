using UnityEngine;

public class LossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGameObject = collision.gameObject;
        if (!colGameObject.CompareTag("Ball")) return;

        Persistent.Instance.GameManager.Lose();

        // destroy the ball >:[
        Destroy(colGameObject);
    }
}
