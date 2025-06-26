using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public void ChangeSkybox(Material skyboxMat)
    {
        RenderSettings.skybox = skyboxMat;
    }
}