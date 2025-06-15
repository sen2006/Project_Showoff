using UnityEngine;

public class ShutterController : MonoBehaviour
{
    [SerializeField]
    private KeyCode takePhotoKey = KeyCode.Space; // Key to trigger the shutter animation

    [SerializeField]
    private Animator shutterAnimator;
    [SerializeField]
    AudioSource shutterSound;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(takePhotoKey))
        {
            shutterAnimator.SetTrigger("Shutter");
            if (shutterSound != null) shutterSound.Play(); // if assigned
        }
    }
}
