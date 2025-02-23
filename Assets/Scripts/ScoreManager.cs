using UnityEngine;
using TMPro; // TextMeshPro namespace
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    // Store the individual scores for each shot
    private int[] scores = new int[10];  // To store up to 10 shot scores (you can change this if needed)
    private int scoreIndex = 0;  // Current index for the score (incremented after each shot)

    // Track the total score
    private int totalScore = 0;
    
    // Track the current round score (this is the score for the current round)
    private int currentScore = 0;

    // Reference to TextMeshPro component for displaying the score
    public TextMeshProUGUI scoreText;  // Assign the TextMeshPro UI Text here
    public TextMeshProUGUI totalScoreText;  // To display the total score after 10 shots

    public GameObject targetPrefab;  // Assign your Target Prefab in the Inspector
    public Transform spawnPoint;     // Assign a spawn point in the Inspector
    private GameObject currentTarget;

    void Start()
    {
        // Ensure a target is present at the beginning of the game
        if (currentTarget == null)
        {
            SpawnNewTarget();
        }
        // Initialize the score display
        UpdateScoreDisplay();
    }

    // Add score for the current shot
    public void AddScore(int score)
    {
        // Add score for the current shot
        if (scoreIndex < scores.Length)
        {
            scores[scoreIndex] = score;  // Store the score for this shot
            scoreIndex++;  // Move to the next shot position
        }
        else
        {
            Debug.LogWarning("Score array is full!");
            return;

        }


        // Update the current round score and total score
        currentScore += score;  // Add to current round score
        totalScore += score;    // Add to total score

        
        // Update the UI after every shot
        UpdateScoreDisplay();
    }

    // Reset the score for the current round
    public void ResetScore()
    {
        // Reset current round and total scores
        currentScore = 0;  // Reset current round score
        totalScore = 0;    // Reset total score
        scoreIndex = 0;    // Reset the shot count

        // Clear the individual shot scores
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }

        // Destroy the current target
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        // Spawn a new target
        SpawnNewTarget();

        // Update the score display
        UpdateScoreDisplay();
    }

    // Get the total score after all shots
    public int GetTotalScore()
    {
        return totalScore; // Return the total score after 10 shots
    }

    // Update the score UI with the current round and total score
    private void UpdateScoreDisplay()
    {
        // Display the individual scores for each shot
        string scoreDisplay = "Scores:\n";

        for (int i = 0; i < scoreIndex; i++)
        {
            scoreDisplay += "Shot " + (i + 1) + ": " + scores[i] + "\n";
        }

        // Update the current round score
        if (scoreText != null)
        {
            scoreText.text = scoreDisplay + "\nTotal Score: " + totalScore;
        }

        // Update the total score UI (optional, you can choose what you prefer)
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: " + totalScore;
        }
    }

    private void SpawnNewTarget()
    {
        currentTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}