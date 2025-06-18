using UnityEngine;

public class ShutterController : MonoBehaviour
{
    [SerializeField] Animator shutterAnimator;
    [SerializeField] AudioSource shutterSound;

    public void trigger() {
        shutterAnimator.SetTrigger("Shutter");
        if (shutterSound != null) shutterSound.Play();
    }
}
