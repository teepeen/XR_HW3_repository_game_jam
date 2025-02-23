using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TargetHit : MonoBehaviour
{
    public Transform targetCenter; // Assign this in the Inspector (exact center of the target)

    // Define scoring zones (adjust based on target size)
    public float bullseyeExactRadius = 0.05f;  // Very small radius for the exact bullseye (highest score)
    public float bullseyeRadius = 0.1f;        // Regular bullseye (10 points)
    public float innerRingRadius = 0.3f;       // Inner ring (5 points)
    public float middleRingRadius = 0.5f;      // Middle ring (2 points)

    private ScoreManager scoreManager;  // Reference to ScoreManager script

    void Start()
    {
        // Get the ScoreManager component in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject arrow = collision.gameObject; // Get the arrow that hit

            // Get the contact point where the arrow hit
            Vector3 hitPoint = collision.contacts[0].point;

            // Calculate distance from target center
            float distance = Vector3.Distance(hitPoint, targetCenter.position);

            // Determine the score based on the distance from the target center
            int score = CalculateScore(distance);

            // Add score to the ScoreManager
            if (scoreManager != null)
            {
                scoreManager.AddScore(score);
            }

            // Debug log the hit result
            Debug.Log($"Arrow hit at {hitPoint}! Distance: {distance}, Score: {score}");
        }
    }

    private int CalculateScore(float distance)
    {
        if (distance <= bullseyeExactRadius) return 50;    // Exact center (highest score)
        if (distance <= bullseyeRadius) return 10;         // Regular bullseye
        if (distance <= innerRingRadius) return 5;         // Inner ring
        if (distance <= middleRingRadius) return 2;        // Middle ring
        if (distance >  middleRingRadius) return 0;
        Debug.Log($"Arrow missed target area, Score: 0");
        return 0;
    }
}