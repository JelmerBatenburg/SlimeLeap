using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed;

    public void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
