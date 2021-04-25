using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CoinsCollected coinsCollected;
    [SerializeField] GameObject CoinsCollected_Object;
    [SerializeField] private Rigidbody2D rb;

    Animator anim;

    [SerializeField] public float timeRemaining_holder;
    [SerializeField] private float timeRemaining;
    [SerializeField] private int No_JumpsToDie;

    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private bool _Moving = true;
    [SerializeField] private bool Is_AI;

    [SerializeField] private float Moving_Speed;
    [SerializeField] private int speed_1;
    [SerializeField] private int speed;
    [SerializeField] private Transform player;

    bool facingRight;
    private Transform target;
    private Vector2 movement;

    private void Start()
    {
        coinsCollected = CoinsCollected_Object.GetComponent<CoinsCollected>();
        CoinsCollected_Object = GameObject.Find("CoinsCollected");
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        timerIsRunning = true;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Is_AI == false)
        {
            if (_Moving == true)
            {
                rb.velocity = new Vector2(speed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(speed_1, 0f);
            }
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

        Vector3 Direction = player.position - transform.position;
        Direction.Normalize();
        movement = Direction;

        if (Vector3.Distance(target.position, transform.position) < 20)
        {
            if (target.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (target.position.x < transform.position.x && facingRight)
                Flip();
        }
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition ((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    private void FixedUpdate()
    {
        if (Is_AI == true)
        {
            MoveEnemy(movement);
        }
    }
    public void PlayerTurn()
    {
        coinsCollected.EnableAvailable_Jumps();
        anim.SetBool("IsWalkin", false);
        timerIsRunning = true;
        playerController.End_OfLevel();
        playerController.Update();
        playerController.NoIcon_Deactivate();
        playerController.CanJump = true;
        timeRemaining_holder = timeRemaining; //resets remainin time back to orijinal time remainin value

        gameObject.GetComponent<EnemyController>().enabled = false;

        if(gameObject.GetComponentInChildren<ShootProjectile>() == null)
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
        if (Is_AI == false)
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
}
