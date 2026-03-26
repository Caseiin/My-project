using UnityEngine;

public class TrajectoryTester : MonoBehaviour
{
    [SerializeField] TrajectorPredictor predictor; // on the hand child
    [SerializeField] Transform throwPoint;         // usually same as hand

    [SerializeField] float throwForce = 10f;

    void Update()
    {
        // Predict trajectory while aiming
        Vector3 startPos = throwPoint.position;
        Vector3 startVelocity = throwPoint.forward * throwForce;
        predictor.Predict(startPos, startVelocity);

        // Throw on input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProjectileThrow thrower = GetComponent<ProjectileThrow>();
            thrower.Throw();
        }
    }
}
