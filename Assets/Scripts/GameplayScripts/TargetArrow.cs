using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public Transform target;
    public Transform ship;
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - ship.position), rotationSpeed * Time.deltaTime);  
    }
}
