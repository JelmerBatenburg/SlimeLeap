using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Transform pipeSpawnPoint;
    public float spawnForce;
    public GameObject playerObject;
    public GameObject currentPlayerObject;
    public float spawnDelay;

    public Transform endPipe;
    public float endPipeSize;
    public LayerMask playerMask;
    public string playerString;

    public Animator finishDisplay;
    public GameObject options;
    public float optionsDelay;
    public AudioClip teleport;
    public AudioSource source;

    public void Start()
    {
        StartCoroutine(SpawnPlayer(spawnDelay));
    }

    public void Update()
    {
        if (Physics.CheckSphere(endPipe.position, endPipeSize, playerMask))
        {
            finishDisplay.SetTrigger("Finish");
            Destroy(GameObject.FindWithTag(playerString));
            StartCoroutine(OptionDisplay(optionsDelay));
            source.PlayOneShot(teleport);
        }
        if(Input.GetButtonDown("Cancel"))
        {
            options.SetActive(!options.activeInHierarchy);
            currentPlayerObject.GetComponent<SlimeJump>().disableJump = options.activeInHierarchy;
        }
    }

    public IEnumerator OptionDisplay(float time)
    {
        yield return new WaitForSeconds(time);
        options.SetActive(true);
    }

    public IEnumerator SpawnPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        currentPlayerObject = Instantiate(playerObject, pipeSpawnPoint.position, Quaternion.identity);
        currentPlayerObject.GetComponent<SlimeJump>().force += pipeSpawnPoint.forward * spawnForce;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(endPipe.position, endPipeSize);
    }
}
