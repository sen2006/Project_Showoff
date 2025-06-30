using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RotationPlanets : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation speed in degrees per second.")]
    public float rotationSpeed = 25f;

    [Tooltip("Rotation axis. Use (1, 0, 0) for X, (0, 1, 0) for Y, (0, 0, 1) for Z, or combinations.")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    void Update()
    {
        // Rotate around the Y-axis at rotationSpeed degrees per second
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
