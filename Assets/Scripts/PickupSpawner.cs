using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BallSlowPrefab;
    [SerializeField] private GameObject DoubleScorePrefab;

    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;

    [SerializeField] private float xAxisPosLimit;
    [SerializeField] private float yAxisPosLimit;

    private float timeUntilNextPickup;

    private void Start()
    {
        SetNextPickupTimer();
    }

    private void SetNextPickupTimer()
    {
        timeUntilNextPickup = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void SpawnPickup(GameObject pickup)
    {
        GameObject newPickup = Instantiate(pickup, transform);

        float posX = Random.Range(-xAxisPosLimit, xAxisPosLimit);
        float posY = Random.Range(-yAxisPosLimit, yAxisPosLimit);
        newPickup.transform.position = new Vector3(posX, posY, 0);
    }

    void Update()
    {
        // dont spawn until ball starts to move
        if (!Persistent.Instance.GameManager.ballStartedMoving) return;

        if (timeUntilNextPickup <= 0)
        {
            SpawnPickup(BallSlowPrefab);

            // spawn double score along with ball slow with a 50% chance
            if (Random.value < 0.5f) SpawnPickup(DoubleScorePrefab);

            SetNextPickupTimer();
        }

        timeUntilNextPickup -= Time.deltaTime;
    }
}
