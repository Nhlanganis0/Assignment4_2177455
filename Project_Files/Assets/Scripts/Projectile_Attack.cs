using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Attack : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerStatus;
    [SerializeField]private float projectileSpeed = 10.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        rb.velocity = transform.TransformDirection(Vector3.right * projectileSpeed);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            print("Player");
            playerStatus.playerHealth = playerStatus.playerHealth - 2;
            Destroy(gameObject);
        }
    }
}
