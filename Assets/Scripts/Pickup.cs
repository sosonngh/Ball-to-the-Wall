using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float lifespan;

    private float animationSpeed = 5;
    private float animationHeight = 0.05f;

    private Vector3 startingPos;
    private float animationOffset;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        Activate(collision.gameObject);

        collision.GetComponent<Ball>().PlayPickupSound();

        Destroy(gameObject);
    }

    protected abstract void Activate(GameObject collision);

    private void Start()
    {
        startingPos = transform.position;
        animationOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void Update()
    {
        if (lifespan <= 0)
        {
            Destroy(gameObject);
            return;
        }

        lifespan -= Time.deltaTime;

        // FLOAT ANIMATION

        float offset = Mathf.Sin(Time.time * animationSpeed + animationOffset) * animationHeight;

        transform.position = startingPos + Vector3.up * offset;
    }
}
