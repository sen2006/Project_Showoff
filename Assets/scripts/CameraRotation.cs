using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Transform tf;

    float angleVert;
    float angleHor;

    void Start()
    {
        tf = transform;
    }

    void Update()
    {
        angleVert -= Input.GetAxis("Mouse Y");
        angleHor += Input.GetAxis("Mouse X");


        if (angleVert > 90) angleVert = 90;
        else if (angleVert < -90) angleVert = -90;

        tf.localRotation = Quaternion.Euler(angleVert, angleHor, 0);
    }
}
