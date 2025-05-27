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

    [Header("Photo Settings")]
    [SerializeField] KeyCode takePhotoKey;
    [SerializeField] string screenshotPath = "screenshots/";

    private Camera _camera;
    CameraVisibilityChecker _visibilityChecker;

    private void Start()
    {
        _camera = cam.GetComponent<Camera>();
        _visibilityChecker = cam.GetComponent<CameraVisibilityChecker>();
        Debug.Assert(cart != null, "Attach the cart to the player script");
        Debug.Assert(cam != null, "Attach the camera to the player script");
        Debug.Assert(_visibilityChecker != null, "The camera object has no cameraVisibilityChecker assigned");
        Debug.Assert(_camera != null, "The camera object has no camera assigned");
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
        if (Input.GetKeyDown(takePhotoKey)) takePhoto();
    }

    private void playerRotation()
    {
        float cartYaw = cart.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cartYaw + yaw, 0);
        cam.transform.localRotation = Quaternion.Euler(-pitch, 0, 0);
    }

    private void takePhoto()
    {

        string folderPath = Application.persistentDataPath+"/"+screenshotPath;

        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName =System.DateTime.Now.ToString("yyyyMMdd-HHmmss_") + _visibilityChecker.getScore() +".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 2);
        Debug.Log(folderPath + screenshotName);

    }

}
