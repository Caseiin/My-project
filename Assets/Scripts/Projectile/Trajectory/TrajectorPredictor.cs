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

        Vector3 pos = startPos;
        Vector3 vel = startVelocity;

        for(int i = 0; i < steps; i++)
        {
            points[i] = pos;

            vel += Physics.gravity * timeStep;
            pos += vel * timeStep;
        }

        line.positionCount = steps;
        line.SetPositions(points);
    }

    public void StopPrediction()
    {
        line.enabled = false;
    }
}
