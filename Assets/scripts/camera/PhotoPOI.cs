using System.Collections.Generic;
using UnityEngine;

public class PhotoPOI : MonoBehaviour {
    [SerializeField] int pointReward = 1;
    [SerializeField] string POIName = null;

    [SerializeField,ReadOnly] public static List<PhotoPOI> POIs = new List<PhotoPOI>();

    private void Start()
    {
        POIs.Add(this);
    }

    private void OnDestroy()
    {
        POIs.Remove(this);
    }

    public int GetPoints() {
        return pointReward;
    }
    public string GetName() {
        return POIName;
    }
}
