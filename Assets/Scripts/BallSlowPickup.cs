using UnityEngine;

public class BallSlowPickup : Pickup
{
    [SerializeField] private float speedDelta;
    
    protected override void Activate(GameObject collision)
    {
        collision.GetComponent<Ball>().ChangeSpeed(speedDelta);
    }
}
