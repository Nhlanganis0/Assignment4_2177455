using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject target;
    public float range;
    public float fireingRate;

    public GameObject projectile;

    private Transform projectileSpawnPoint;
    private bool canShoot = true;
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");

        }
        projectileSpawnPoint = transform.Find("Projectile Spawn Point");
    }

    void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.transform.position.x, target.transform.position.y)) <= range)
        {
            if (canShoot)
            {
                StartCoroutine(FireProjectile());
            }
        }
    }

    IEnumerator FireProjectile()
    {
        Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        canShoot = false;
        yield return new WaitForSeconds(fireingRate);
        canShoot = true;
    }
}
