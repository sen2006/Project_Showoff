using UnityEngine;

public class randomscript : MonoBehaviour
{
    [SerializeField] GameObject cart;
    [SerializeField] GameObject playerCamera;
    [SerializeField, ReadOnly] float yaw = 0;

    [SerializeField, ReadOnly] float pitch = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cartYaw = cart.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cartYaw + yaw + 90, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(-pitch, 0, 0);
    }
}
