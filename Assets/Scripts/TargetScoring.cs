using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TargetScoring : MonoBehaviour
{
    [System.Serializable]
    public class ScoreZone
    {
        public Collider collider; // Assign collider for scoring
        public int score; // Score for this zone
    }

    public ScoreZone[] scoreZones; // Array of different scoring zones

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow")) // Make sure it's an arrow
        {
            GameObject arrow = collision.gameObject;
            Transform arrowTip = arrow.transform; // The front of the arrow

            // Cast a ray forward from the arrow tip to check which colliders it passes through
            RaycastHit[] hits = Physics.RaycastAll(arrowTip.position, arrowTip.forward, 1.5f);

            List<ScoreZone> hitZones = new List<ScoreZone>();

            foreach (RaycastHit hit in hits)
            {
                foreach (ScoreZone zone in scoreZones)
                {
                    if (hit.collider == zone.collider)
                    {
                        hitZones.Add(zone);
                        Debug.Log("Arrow passed through: " + zone.collider.name + " | Score: " + zone.score);
                    }
                }
            }

            if (hitZones.Count > 0)
            {
                // Choose the best scoring zone (highest score)
                ScoreZone bestZone = hitZones.OrderByDescending(z => z.score).First();

                Debug.Log("Best scoring zone hit: " + bestZone.collider.name + " | Score: " + bestZone.score);

                // Make the arrow stick at the impact point of the best scoring zone
                StickArrow(arrow, bestZone.collider);
            }
        }
    }

    private void StickArrow(GameObject arrow, Collider bestCollider)
    {
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true; // Stop physics so it sticks
        }

        // Move the arrow to the correct position (surface of the correct collider)
        arrow.transform.position = bestCollider.ClosestPoint(arrow.transform.position);
        arrow.transform.rotation = Quaternion.LookRotation(-arrow.transform.forward);
    }
}