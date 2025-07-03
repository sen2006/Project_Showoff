using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RotationPlanets : MonoBehaviour
{
    public float rotationSpeed = 35f;

    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    void Update()
    {
        // Rotate around the Y-axis at rotationSpeed degrees per second
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
