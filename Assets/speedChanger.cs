using Unity.Cinemachine;
using UnityEngine;

public class speedChanger : MonoBehaviour
{
    [SerializeField]CinemachineSplineCart cart;
    [SerializeField]float newSpeed;
    public void trigger()
    {
        ((SplineAutoDolly.FixedSpeed)cart.AutomaticDolly.Method).Speed = newSpeed;
    }
}
