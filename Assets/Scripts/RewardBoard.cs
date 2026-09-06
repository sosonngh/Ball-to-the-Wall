using UnityEngine;

public class RewardBoard : MonoBehaviour
{
    [SerializeField] private Sprite[] rewardBoardSprites;

    private void Start()
    {
        int currentSpriteIndex = Persistent.Instance.GameManager.currentMilestoneIndex;
        GetComponent<SpriteRenderer>().sprite = rewardBoardSprites[currentSpriteIndex];
    }
}