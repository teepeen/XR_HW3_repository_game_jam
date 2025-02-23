using UnityEngine;

public class TwoHandedBow : MonoBehaviour
{
    public Transform stringStart;  // The start of the string (near the bow)
    public Transform stringEnd;    // Where the string is pulled to (near the second hand)
    public MeshRenderer bowMeshRenderer; // MeshRenderer to access the bow mesh

    private Mesh bowMesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    private float tension;  // Pull tension of the bowstring

    void Start()
    {
        bowMesh = bowMeshRenderer.GetComponent<MeshFilter>().mesh;  // Get the mesh of the bow
        originalVertices = bowMesh.vertices;  // Store the original vertices
        modifiedVertices = new Vector3[originalVertices.Length];  // Create a modified vertices array
    }

    void Update()
    {
        // Calculate tension based on the second hand's position
        tension = Vector3.Distance(stringStart.position, stringEnd.position);
        tension = Mathf.Clamp(tension, 0, 1);  // Limit the tension

        // Move the string vertices based on tension
        ModifyStringVertices();
        bowMesh.vertices = modifiedVertices;  // Apply the modified vertices back to the mesh
        bowMesh.RecalculateBounds();  // Recalculate bounds to avoid clipping issues
    }

    void ModifyStringVertices()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            // Here, we modify only the vertices that belong to the string part
            // You can define a threshold or condition to find string vertices
            if (IsStringVertex(vertex))
            {
                // Simulate tension by moving the string vertices towards the end
                // This example linearly interpolates the vertices
                vertex = Vector3.Lerp(vertex, stringEnd.position, tension);
            }

            modifiedVertices[i] = vertex;  // Store the modified vertex
        }
    }

    bool IsStringVertex(Vector3 vertex)
    {
        // Simple logic to determine if the vertex is part of the string (could be more complex)
        // For now, let's assume any vertex near the string's starting point is part of the string.
        return (vertex - stringStart.position).magnitude < 0.1f;
    }
}