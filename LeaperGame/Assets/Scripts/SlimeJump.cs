using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJump : MonoBehaviour
{
    public float jumpForce;
    public bool disableJump;
    public Vector3 force;
    public bool onWall;
    public float gravity;
    public float range;
    public float airDrag, wallDrag;
    public MeshFilter filter;
    public Mesh flyingMesh, idleMesh;
    public Transform body;
    public Transform arrow, point;
    public AudioClip[] jumpClips, impactClips;
    public PlayAudioRandomizer audioSource;

    public void Update()
    {
        if (!disableJump)
        {
            if (Input.GetButtonDown("Fire1") && onWall)
                StartCoroutine(Leap());
            if (!onWall)
                CheckForWallCollision();
            ForceChanges();
        }
    }

    public void SetOnGroundMesh(bool onground)
    {
        filter.mesh = onground ? idleMesh : flyingMesh;
    }

    public void ForceChanges()
    {
        if (!onWall)
        {
            force += Vector3.down * gravity * Time.deltaTime;
            body.LookAt(transform.position + force);
        }
        force = Vector3.Lerp(force, Vector3.zero, Time.deltaTime * airDrag);
        transform.position += force * Time.deltaTime;
    }

    public void CheckForWallCollision()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position - (force.normalized * 0.05f), force, out hit, range))
        {
            transform.position = hit.point;
            onWall = true;
            force.x = 0;
            force.y = 0;
            SetOnGroundMesh(true);
            body.LookAt(hit.point - hit.normal);
            transform.parent = hit.transform;
            audioSource.PlaySound(impactClips);
        }
    }

    public IEnumerator Leap()
    {
        Vector3 mouseStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        arrow.gameObject.SetActive(true);
        point.gameObject.SetActive(true);
        point.transform.position = Camera.main.ScreenToWorldPoint(mouseStartPos);
        Vector3 mouseEndPos = new Vector3();
        Vector3 leapDirection = new Vector3();
        while (Input.GetButton("Fire1"))
        {
            point.transform.position = Camera.main.ScreenToWorldPoint(mouseStartPos);
            mouseEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            leapDirection = (mouseStartPos - mouseEndPos).normalized;
            leapDirection.z = 0;
            arrow.LookAt(transform.position - leapDirection);
            yield return null;
        }
        arrow.gameObject.SetActive(false);
        point.gameObject.SetActive(false);
        if (onWall)
        {
            audioSource.PlaySound(jumpClips);
            force += leapDirection * jumpForce;
            onWall = false;
            transform.parent = null;
            SetOnGroundMesh(false);
        }

    }
}
