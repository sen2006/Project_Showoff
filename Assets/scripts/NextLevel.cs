using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEnter;
    [SerializeField]
    bool showCursorOnNextScene = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Tag your player object as "Player"
        {
            if (showCursorOnNextScene)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            onTriggerEnter.Invoke();
        }
    }
}
