using UnityEngine;

public class DoubleScorePickup : Pickup
{
    [SerializeField] private float duration;

    protected override void Activate(GameObject collision)
    {
        Persistent.Instance.GameManager.EnableDoubleScore(duration);
    }
}
