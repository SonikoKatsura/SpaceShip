using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateItem : MonoBehaviour
{
    public bool rotate; // do you want it to rotate?
    public float rotationSpeed;

    void Update()
    {

        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

    }

}
