using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnvironmentTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Tag your player object as "Player"
        {
            onTriggerEnter.Invoke();
        }
    }
}
