using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class CameraVisibilityChecker : MonoBehaviour
{
    Camera cam;

    public void Awake()
    {
        cam = GetComponent<Camera>();
        //Debug.Log(cam);
    }

    private void Update()
    {
        List<PhotoPOI> visiblePOIs = new List<PhotoPOI>();

        Renderer[] allRenderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        //Debug.Log(cam);
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        foreach (Renderer renderer in allRenderers)
        {
            if (renderer.isVisible && GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds) &&
                renderer.gameObject.GetComponent<PhotoPOI>() != null)
            {
                visiblePOIs.Add(renderer.gameObject.GetComponent<PhotoPOI>());
            }
        }

        for (int i = visiblePOIs.Count - 1; i >= 0; i--)
        {
            PhotoPOI POI = visiblePOIs[i];
            Vector3 dif = POI.gameObject.transform.position - cam.transform.position;
            if (Physics.Raycast(cam.transform.position, dif.normalized, dif.magnitude, LayerMask.GetMask("Block View")))
            {
                visiblePOIs.Remove(POI);
            }
        }
  
        int achievedScore = 0;
        foreach (PhotoPOI POI in visiblePOIs)
        {
            achievedScore += POI.GetPoints();
        }

        Debug.Log("Visible objects count: " + visiblePOIs.Count);
        Debug.Log("Visible objects score: " + achievedScore);
    }
}
