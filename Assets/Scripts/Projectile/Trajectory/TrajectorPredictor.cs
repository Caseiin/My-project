using UnityEngine;

public class TrajectorPredictor : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] int steps = 30;
    [SerializeField] float timeStep = 0.1f; 

    public void Predict(Vector3 startPos, Vector3 startVelocity)
    {
        line.enabled = true;

        Vector3[] points = new Vector3[steps];

        Vector3 currentPos = startPos;
        Vector3 currentVel = startVelocity;

        for (int i = 0; i < steps; i++)
        {
            points[i] = currentPos;

            // Predict next position
            Vector3 nextVel = currentVel + Physics.gravity * timeStep;
            Vector3 nextPos = currentPos + nextVel * timeStep;

            Vector3 segment = nextPos - currentPos;
            if (Physics.Raycast(currentPos, segment.normalized, out RaycastHit hit, segment.magnitude))
            {
                points[i] = hit.point;

                // Clamp rest of line to hit point
                for (int j = i + 1; j < steps; j++)
                    points[j] = hit.point;

                break;
            }

            currentPos = nextPos;
            currentVel = nextVel;
        }

        line.positionCount = steps;
        line.SetPositions(points);
    }

    public void StopPrediction()
    {
        line.enabled = false;
    }
}
