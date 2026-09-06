using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private int highscore;
    [SerializeField] private int currentScore;
    [SerializeField] private bool isDoubleScoreActive;
    [SerializeField] private int scorePerHit;
    [SerializeField] private float scoreMultiplier;

    [SerializeField] private int[] scoreMilestones;
    [SerializeField] private string[] milestoneRewards;

    public int currentMilestoneIndex;
    public bool isGameActive;
    public bool ballStartedMoving;
    public int scoreGainedThisHit;

    private int lastMilestoneIndex; // index of the last milestone before current game
    // track highscore to display in loss message
    private bool isHighscoreBeaten;

    private Coroutine doubleScoreCoroutine;

    private GameCanvas gameCanvas;

    private void Start()
    {
        gameCanvas = Persistent.Instance.GameCanvas;

        gameCanvas.SetCurrentScore(currentScore, scoreMilestones[currentMilestoneIndex]);

        // load saved data
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        currentMilestoneIndex = PlayerPrefs.GetInt("CurrentMilestoneIndex", 0);

        // set loaded highscore and current milestone in hud
        gameCanvas.SetHighscore(highscore);

        if (currentMilestoneIndex >= scoreMilestones.Length)
        {
            gameCanvas.SetCurrentScore(currentScore);
        }
        else
        {
            gameCanvas.SetCurrentScore(currentScore, scoreMilestones[currentMilestoneIndex]);
        }

        lastMilestoneIndex = currentMilestoneIndex;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Lose()
    {

        if (currentMilestoneIndex > lastMilestoneIndex)
        {
            // add unlocked rewards
            gameCanvas.AddNewRewardsMsg(milestoneRewards[lastMilestoneIndex..currentMilestoneIndex]);
            
            lastMilestoneIndex = currentMilestoneIndex;
        }

       if (isHighscoreBeaten)
        {
            gameCanvas.AddNewHighscoreMsg(highscore);
        }

        isGameActive = false;
        isDoubleScoreActive = false;
        StopAllCoroutines();

        gameCanvas.ShowLossScreen();

    }

    public void Restart()
    {
        isGameActive = true;
        isHighscoreBeaten = false;
        currentScore = 0;
        scoreMultiplier = 1;

        gameCanvas.SetScoreMultiplier(scoreMultiplier);
        gameCanvas.HideLossScreen();
        gameCanvas.ResetRestartMessage();
        gameCanvas.SetHighscore(highscore);

        // check for milestones
        if (currentMilestoneIndex >= scoreMilestones.Length)
        {
            gameCanvas.SetCurrentScore(currentScore);
        }
        else
        {
            gameCanvas.SetCurrentScore(currentScore, scoreMilestones[currentMilestoneIndex]);
        }

        // restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GainScore()
    {
        scoreGainedThisHit = (int)(scorePerHit * scoreMultiplier);

        currentScore += scoreGainedThisHit;

        if (currentScore > highscore)
        {
            highscore = currentScore;
            isHighscoreBeaten = true;

            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }

        // check for milestones passed after this hit
        while (currentMilestoneIndex < scoreMilestones.Length && 
            currentScore >= scoreMilestones[currentMilestoneIndex])
        {
            currentMilestoneIndex++;
        }

        PlayerPrefs.SetInt("CurrentMilestoneIndex", currentMilestoneIndex);
        PlayerPrefs.Save();

        if (currentMilestoneIndex >= scoreMilestones.Length)
        {
            gameCanvas.SetCurrentScore(currentScore);
        }
        else
        {
            gameCanvas.SetCurrentScore(currentScore, scoreMilestones[currentMilestoneIndex]);
        }
        
    }

    public void UpdateScoreMultiplier(float ballCurrentSpeed, float ballBaseSpeed)
    {
        scoreMultiplier = 1 + (ballCurrentSpeed - ballBaseSpeed) / 10;

        if (isDoubleScoreActive) scoreMultiplier *= 2;

        gameCanvas.SetScoreMultiplier(scoreMultiplier);
    }

    public void EnableDoubleScore(float duration)
    {
        if (doubleScoreCoroutine != null)
            StopCoroutine(doubleScoreCoroutine);

        doubleScoreCoroutine = StartCoroutine(DoubleScoreTimer(duration));
    }

    private IEnumerator DoubleScoreTimer(float duration)
    {
        isDoubleScoreActive = true;

        yield return new WaitForSeconds(duration);

        isDoubleScoreActive = false;

        doubleScoreCoroutine = null;
    }

}
