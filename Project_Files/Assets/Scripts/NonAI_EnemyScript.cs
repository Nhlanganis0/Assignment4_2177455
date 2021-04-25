using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonAI_EnemyScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CoinsCollected coinsCollected;
    [SerializeField] private Rigidbody2D rb;
    Animator anim;

    [SerializeField] private float timeRemaining_holder;
    [SerializeField] private float timeRemaining;

    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private bool _Moving = true;

    [SerializeField] private float Moving_Speed;
    [SerializeField] private int speed_1;
    [SerializeField] private int speed;

    bool facingRight;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timerIsRunning = true;
    }
    private void Update()
    {
        if (_Moving == true)
        {
            rb.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(speed_1, 0f);
        }
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                rb.velocity = new Vector2(0f, 0f);
                timeRemaining = timeRemaining_holder;
                PlayerTurn();
            }
        }

        if (Vector3.Distance(target.position, transform.position) < 20)
        {
            if (target.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (target.position.x < transform.position.x && facingRight)
                Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    public void PlayerTurn()
    {
        coinsCollected.EnableAvailable_Jumps();
        anim.SetBool("IsWalkin", false);
        timerIsRunning = true;
        playerController.End_OfLevel();
        playerController.Update();
        playerController.NoIcon_Deactivate();
        timeRemaining_holder = timeRemaining; //resets remainin time back to orijinal time remainin value
        gameObject.GetComponent<NonAI_EnemyScript>().enabled = false;
        if (coinsCollected.Bought)
        {
            playerController.AddJump(playerController.playerJumps_Available = 3);
            coinsCollected.Bought = false;
        }
        else
        {
            playerController.playerJumps_Available = 3;
        }

        if (gameObject.GetComponentInChildren<ShootProjectile>() == null)
        {
            //do nada
        }
        else
        {
            gameObject.GetComponentInChildren<ShootProjectile>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Patroll"))
        {
            if (_Moving)
            {
                _Moving = false;
            }
            else
            {
                _Moving = true;
            }
        }
    }
}
