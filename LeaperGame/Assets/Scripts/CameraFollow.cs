using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform slime;
    public Vector3 lookLerp;
    public float lerpSpeed, camLerp;
    public float offset;
    public string playerTag;

    public void Update()
    {
        if (slime)
        {
            lookLerp = Vector3.Lerp(lookLerp, slime.position, Time.deltaTime * camLerp);
            transform.LookAt(lookLerp);
            transform.position = Vector3.Lerp(transform.position, slime.position - (Vector3.forward * offset), Time.deltaTime * lerpSpeed);
        }
        else if(GameObject.FindWithTag(playerTag))
            slime = GameObject.FindWithTag(playerTag).transform;
    }
}
