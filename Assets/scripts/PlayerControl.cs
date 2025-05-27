using System.ComponentModel;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject cart;
    [SerializeField] GameObject cam;

    [Header("Camera Settings")]
    [SerializeField] float horizontalMouseSensitivity = 1;
    [SerializeField] float verticalMouseSensitivity = 1;
    [SerializeField, Range(0, 180)] int maxYaw = 180;
    [SerializeField] bool useMaxYaw = true;
    [SerializeField, Range(0, 90)] int maxPitchUp = 80;
    [SerializeField, Range(0, 90)] int maxPitchDown = 80;
    [SerializeField, ReadOnly] float yaw = 0;
    [SerializeField, ReadOnly] float pitch = 0;
    private void Start()
    {
        Debug.Assert(cart != null, "Attach the cart to the player script");
        Debug.Assert(cam != null, "Attach the camera to the player script");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        handleInput();
        playerRotation();
    }

    private void handleInput()
    {
        yaw += Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime * 1000;
        pitch += Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime * 1000;
        if (useMaxYaw) yaw = Mathf.Clamp(yaw, -maxYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, -maxPitchDown, maxPitchUp);
    }

    private void playerRotation()
    {
        float cartYaw = cart.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cartYaw + yaw, 0);
        cam.transform.localRotation = Quaternion.Euler(-pitch, 0, 0);
    }
}
