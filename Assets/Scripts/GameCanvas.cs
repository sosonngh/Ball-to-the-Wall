using System.Linq;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject lossScreen;
    [SerializeField] private TMP_Text lossMessage;
    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private TMP_Text scoreMultiplier;
    [SerializeField] private TMP_Text highscore;

    [SerializeField] private TMP_Text scoreGainedMessagePrefab;

    private RectTransform rectTransform;
    private GameManager gameManager;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        gameManager = Persistent.Instance.GameManager;
    }

    // LOSS SCREEN
    public void ShowLossScreen()
    {
        lossScreen.SetActive(true);
    }

    public void HideLossScreen()
    {
        lossScreen.SetActive(false);
    }

    public void AddNewHighscoreMsg(int highscore)
    {
        lossMessage.text = $"NEW HIGH SCORE!\n{highscore}\n\n" + lossMessage.text;
    }

    public void AddNewRewardsMsg(string[] newRewards)
    {
        lossMessage.text = $"NEW REWARDS!\n{string.Join(", ", newRewards)}\n\n" + lossMessage.text;
    }

    public void ResetRestartMessage()
    {
        lossMessage.text = "PRESS \"R\" TO RESTART";
    }

    // SCORES
    public void SetCurrentScore(int score)
    {
        currentScore.text = $"{score}";
    }

    public void SetCurrentScore(int score, int currentMilestone)
    {
        currentScore.text = $"{score}/{currentMilestone}";
    }

    public void SetHighscore(int highscore)
    {
        this.highscore.text = $"{highscore}";
    }

    public void SetScoreMultiplier(float multiplier)
    {
        scoreMultiplier.text = "X " + multiplier.ToString("F2"); // 2 decimals
    }

    public void DisplayScoreGainedMessage(Vector2 playerWorldPos) // receives the world position of player
    {
        
        TMP_Text scoreGainedMessage = Instantiate(scoreGainedMessagePrefab, transform);

        float posX = Random.Range(0, 2) == 0 ? -1.5f : 1.5f; // either left or right side of the player
        float posY = Random.Range(-1f, 1f);

        scoreGainedMessage.transform.position = playerWorldPos + Vector2.right * posX + Vector2.up * posY;

        // display the value
        scoreGainedMessage.text = "+" + gameManager.scoreGainedThisHit;
    }
}
