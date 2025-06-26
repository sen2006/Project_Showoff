using UnityEngine;

public class DisableFogTrigger : MonoBehaviour
{
    // This function disables the classic RenderSettings fog
    public void DisableFog()
    {
        RenderSettings.fog = false;
        Debug.Log("Fog disabled!");
    }
}
