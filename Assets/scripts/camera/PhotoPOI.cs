using UnityEngine;

public class PhotoPOI : MonoBehaviour
{
    [SerializeField] int pointReward = 1;
    [SerializeField] string POIName = null;

    public int GetPoints()
    {
        return pointReward;
    }

    public string GetName()
    {
        return POIName;
    }
}
