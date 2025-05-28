using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(Camera))]
public class CameraVisibilityChecker : MonoBehaviour {
    Camera cam;

    [Header("Debug Data")]
    [SerializeField] bool updateScoreOnUpdateLoop = false;
    [SerializeField, ReadOnly] int visiblePOICount;
    [SerializeField, ReadOnly] int achievedScore;
    LayerMask layerMask;


    void Awake() {
        cam = GetComponent<Camera>();
        layerMask = LayerMask.GetMask("Block View");
    }

    void Update() {
        if (updateScoreOnUpdateLoop)
            UpdateScore();
    }

    private void UpdateScore() {
        List<PhotoPOI> visiblePOIs = getVisiblePOIS();

        foreach (PhotoPOI POI in visiblePOIs)
            achievedScore += POI.GetPoints();

        visiblePOICount = visiblePOIs.Count;
    }

    /// <summary>
    /// returns a List of all visible POIs
    /// </summary>
    private List<PhotoPOI> getVisiblePOIS() {
        List<PhotoPOI> toReturn = new List<PhotoPOI>();
        // get all renderers in the scene
        Renderer[] allRenderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        // get the frustum planes of the camera
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        // for every renderer chech if it is
        // 1: inside the frustum planes
        // 2: not blocked by a "Block View" tab
        foreach (Renderer renderer in allRenderers) {
            if (renderer.isVisible && GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds) &&
                renderer.gameObject.GetComponent<PhotoPOI>() != null) {
                PhotoPOI POI = renderer.gameObject.GetComponent<PhotoPOI>();
                Vector3 dif = POI.gameObject.transform.position - cam.transform.position;
                if (Physics.Raycast(cam.transform.position, dif.normalized, dif.magnitude, layerMask))
                    toReturn.Add(POI);
            }
        }
        return toReturn;
    }

    /// <summary>
    /// Updates and retrieves the score on camera
    /// </summary>
    public int getScore() {
        UpdateScore();
        return achievedScore;
    }
}
