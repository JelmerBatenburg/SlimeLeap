using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public string managerTag;
    public enum DamageAreaType { Sphere, Box};
    public DamageAreaType damageType;
    public float radius;
    public LayerMask damageMask;
    public GameObject deathParticle;
    public float particleLifetime;

    public void Update()
    {
        switch (damageType)
        {
            case DamageAreaType.Box:
                if (Physics.CheckBox(transform.position, new Vector3(radius, radius, 0.05f), Quaternion.identity, damageMask))
                {
                    foreach (Collider col in Physics.OverlapBox(transform.position, new Vector3(radius, radius, 0.05f), Quaternion.identity, damageMask))
                    {
                        Destroy(Instantiate(deathParticle, col.transform.position, Quaternion.identity), particleLifetime);
                        Destroy(col.gameObject);
                    }
                    Manager manager = GameObject.FindWithTag(managerTag).GetComponent<Manager>();
                    manager.StartCoroutine(manager.SpawnPlayer(manager.spawnDelay));
                }
                break;

            case DamageAreaType.Sphere:
                if (Physics.CheckSphere(transform.position, radius, damageMask))
                {
                    foreach (Collider col in Physics.OverlapSphere(transform.position, radius, damageMask))
                    {
                        Destroy(Instantiate(deathParticle, col.transform.position, Quaternion.identity), particleLifetime);
                        Destroy(col.gameObject);
                    }
                    Manager manager = GameObject.FindWithTag(managerTag).GetComponent<Manager>();
                    manager.StartCoroutine(manager.SpawnPlayer(manager.spawnDelay));
                }
                break;
        }
    }

    public void OnDrawGizmos()
    {
        switch (damageType)
        {
            case DamageAreaType.Box:
                Gizmos.DrawWireCube(transform.position, new Vector3(radius, radius, 0.05f));
                break;

            case DamageAreaType.Sphere:
                Gizmos.DrawWireSphere(transform.position, radius);
                break;
        }
    }
}
