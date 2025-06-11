using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField]
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
