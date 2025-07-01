using UnityEngine;

public class MenuCameraRotator : MonoBehaviour
{
    public Transform[] rotationPoints; // Points around which to rotate in each level
    public float rotationSpeed = 10f;
    public float rotationsBeforeSwitch = 1f;
    public float transitionDuration = 3f;

    private int currentLevelIndex = 0;
    private float rotationAmount = 0f;
    private bool isTransitioning = false;

    private void Update()
    {
        if (rotationPoints.Length == 0 || isTransitioning) return;

        // Rotate around the current point
        Transform target = rotationPoints[currentLevelIndex];
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        rotationAmount += rotationSpeed * Time.deltaTime;

        // Check if it's time to switch
        if (rotationAmount >= 360f * rotationsBeforeSwitch)
        {
            rotationAmount = 0f;
            int nextIndex = (currentLevelIndex + 1) % rotationPoints.Length;
            StartCoroutine(TransitionToNextLevel(nextIndex));
        }
    }

    private System.Collections.IEnumerator TransitionToNextLevel(int nextIndex)
    {
        isTransitioning = true;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        // Move to a new start position slightly offset from the next rotation point
        Transform targetPoint = rotationPoints[nextIndex];
        Vector3 targetPosition = targetPoint.position + (transform.position - rotationPoints[currentLevelIndex].position); // keep same relative offset
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint.position - targetPosition); // look at new center

        float timer = 0f;
        while (timer < transitionDuration)
        {
            float t = timer / transitionDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        currentLevelIndex = nextIndex;
        isTransitioning = false;
    }
}
