using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePop : MonoBehaviour
{
    public float popDelay;
    public float fireForce;
    public float intakeSize;
    public Transform intake, firePoint;
    public Color gizmoColor;
    public LayerMask playerMask;
    public string playerString;
    public PlayAudioRandomizer audioSource;
    public AudioClip suck, pop;

    public void Update()
    {
        if (Physics.CheckSphere(intake.position, intakeSize))
            StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        SlimeJump player = GameObject.FindWithTag(playerString).GetComponent<SlimeJump>();
        player.disableJump = true;
        player.force = new Vector3();
        player.transform.position = firePoint.position;
        player.SetOnGroundMesh(true);
        player.body.LookAt(firePoint.position + firePoint.forward);
        audioSource.PlaySound(suck);
        yield return new WaitForSeconds(popDelay);
        player.SetOnGroundMesh(false);
        player.disableJump = false;
        player.onWall = false;
        player.force = firePoint.transform.forward * fireForce;
        audioSource.PlaySound(pop);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(intake.position, intakeSize);
        Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.forward);
    }
}
