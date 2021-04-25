using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePrefabs : MonoBehaviour
{
    private float secondsToDestroy = 15f;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() //for particle system
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
 
