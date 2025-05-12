using UnityEngine;

public class PhotoPOI : MonoBehaviour
{
    [SerializeField] int pointReward = 1;
    public int GetPoints()
    {
        return pointReward;
    }
}
