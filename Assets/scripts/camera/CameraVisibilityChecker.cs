using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class CameraVisibilityChecker : MonoBehaviour {
    Camera cam;

    [Header("Debug Data")]
    [SerializeField] bool updateScoreOnUpdateLoop = false;
    [SerializeField, ReadOnly] int visiblePOICount;
    [SerializeField, ReadOnly] int achievedScore;
    [SerializeField, ReadOnly] LayerMask layerMask;


    void Awake() {
        cam = GetComponent<Camera>();
        layerMask = LayerMask.GetMask("Block View");
    }

    void Update() {
        if (updateScoreOnUpdateLoop)
            UpdateScore();
    }

    /// <summary>
    /// updates the visible score
    /// </summary>
    private void UpdateScore() {
        List<PhotoPOI> visiblePOIs = getVisiblePOIS();

        achievedScore = 0;
        foreach (PhotoPOI POI in visiblePOIs)
            achievedScore += POI.GetPoints();

        visiblePOICount = visiblePOIs.Count;
    }

    /// <summary>
    /// returns a List of all visible POIs
    /// </summary>
    private List<PhotoPOI> getVisiblePOIS() {
        Debug.Log("list:" + PhotoPOI.POIs.Count);
        List<PhotoPOI> toReturn = new List<PhotoPOI>();
        // get all renderers in the scene
        //Renderer[] allRenderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        // get the frustum planes of the camera
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        // for every renderer chech if it is
        // 1: inside the frustum planes
        // 2: not blocked by a "Block View" tab
        foreach (PhotoPOI POI in PhotoPOI.POIs)
        {
            if (POI.gameObject.activeSelf && GeometryUtility.TestPlanesAABB(frustumPlanes, new Bounds(POI.transform.position,new Vector3(.1f,.1f,.1f))))
            {
                Vector3 dif = (POI.gameObject.transform.position) - cam.transform.position;
                Color ray = !Physics.Raycast(cam.transform.position, dif.normalized, dif.magnitude, layerMask) ? Color.green : Color.red;
                Debug.DrawLine(cam.transform.position, cam.transform.position + dif, ray);
                if (!Physics.Raycast(cam.transform.position, dif.normalized, dif.magnitude, layerMask))
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
