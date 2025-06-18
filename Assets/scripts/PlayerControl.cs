using UnityEngine;
using static UnityEngine.ScreenCapture;

public class PlayerControl : MonoBehaviour {
    [Header("Objects")]
    [SerializeField] GameObject cart;
    [SerializeField] GameObject playerCamera;
    [SerializeField] ShutterController shutterController;

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
    [SerializeField] string takePhotoKey = "TakePhoto";
    [SerializeField] string screenshotPath = "screenshots/";

    private Camera _camera;
    CameraVisibilityChecker _visibilityChecker;

    private void Start() {
        _camera = playerCamera.GetComponent<Camera>();
        _visibilityChecker = playerCamera.GetComponent<CameraVisibilityChecker>();
        Debug.Assert(cart != null, "Attach the cart to the player script");
        Debug.Assert(playerCamera != null, "Attach the camera to the player script");
        Debug.Assert(_visibilityChecker != null, "The camera object has no cameraVisibilityChecker assigned");
        Debug.Assert(_camera != null, "The camera object has no camera assigned");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        handleInput();
        playerRotation();
    }

    /// <summary>
    /// reads and checks input controlls
    /// </summary>
    private void handleInput() {
        yaw += Input.GetAxis("Mouse X") * OptionsMenu.sensitivity * horizontalMouseSensitivity;
        pitch += Input.GetAxis("Mouse Y") * OptionsMenu.sensitivity * verticalMouseSensitivity;
        if (useMaxYaw) yaw = Mathf.Clamp(yaw, -maxYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, -maxPitchDown, maxPitchUp);
        if (Input.GetButtonDown(takePhotoKey)) takePhoto();
    }

    /// <summary>
    /// camera rotation
    /// </summary>
    private void playerRotation() {
        float cartYaw = cart.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cartYaw + yaw, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(-pitch, 0, 0);
    }

    /// <summary>
    /// takes a screenshot and saves it
    /// </summary>
    private void takePhoto()
    {
        if (shutterController != null) { shutterController.trigger(); }
        RenderTexture originalRT = RenderTexture.active;

        // Set up RenderTexture
        RenderTexture photoRT = new RenderTexture(Screen.width, Screen.height, 24);
        _camera.targetTexture = photoRT;

        // Render manually to display the new texture (else it becomes black)
        _camera.Render();

        // Activate the RenderTexture and read the pixels
        RenderTexture.active = photoRT;
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        photo.Apply();

        // Save to file
        string folderPath = Application.persistentDataPath + "/" + screenshotPath;
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
        string screenshotName = System.DateTime.Now.ToString("yyyyMMdd-HHmmss_") + _visibilityChecker.getScore() + ".png";
        byte[] bytes = photo.EncodeToPNG();
        System.IO.File.WriteAllBytes(System.IO.Path.Combine(folderPath, screenshotName), bytes);
        Debug.Log("Screenshot taken: " + System.IO.Path.Combine(folderPath, screenshotName));

        // put the original RenderTexture back
        RenderTexture.active = originalRT;

        // Clean up (GPU managed stuff has to be destroyed)
        _camera.targetTexture = null;
        Destroy(photoRT);
        Destroy(photo);
    }

}
