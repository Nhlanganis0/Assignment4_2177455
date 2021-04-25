using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip PlayerDamage;
    [SerializeField] private AudioClip DeathSound;

    [SerializeField] CoinsCollected coinsCollected;
    Animator anim;
    [SerializeField] private GameObject EnemyDeath_Effect;
    [SerializeField] private GameObject Player_NoIcon;
    [SerializeField] private GameObject Enemy_NoIcon;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private GameObject EnemyToken;
    [SerializeField] private GameObject Platforms;
    [SerializeField] private GameObject PlayAgain;
    [SerializeField] private GameObject Health_1;
    [SerializeField] private GameObject Health_2;
    [SerializeField] private GameObject Health_3;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject Store_Question;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Button Right;
    [SerializeField] private Button Space;
    [SerializeField] private Button Left;
    [SerializeField] public GameObject[] Enemies;

    [SerializeField] Camera NextLevel_Cam;
    [SerializeField] Camera CurrentLevel_Cam;

    [SerializeField] private int No_Of_JumpsToKill_Enemies;
    [SerializeField] public int playerJumps_Available;
    [SerializeField] private int Tokens_ToWin;
    [SerializeField] private int JumpsValue;
    [SerializeField] float EnemyKillJump;
    [SerializeField] float ResetJumpsToKillEnemy_Duration;
    [SerializeField] private int Jumps;
    [SerializeField] float jumpForce;
    [SerializeField] float runSpeed;

    public bool CanJump = true; 
    
    public int TokensCollected;
    private float checkRadius;
    public int playerHealth;

    int DeadEnemies;
    [SerializeField]private int No_OfDeadEnemies_ToWinLevel;

    [SerializeField] private LayerMask whatIsGround;
    private ScreenShake shake;
    private bool isGrounded;

   
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Player_NoIcon = GameObject.FindGameObjectWithTag("No_Icon");
        shake = GameObject.FindGameObjectWithTag("Screenshake").GetComponent<ScreenShake>();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        DeadEnemies = 0;
        playerHealth = 8;
        playerJumps_Available = 3;
        anim = GetComponent<Animator>();
    }
   
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
    }

    public void Update()
    {
        Damage();
        Movement();
        DoubleJump();
        GroundChecker();
        End_OfLevel();
    }

    public void End_OfLevel()
    {
        if(DeadEnemies == No_OfDeadEnemies_ToWinLevel )
        {
            StartCoroutine(DelayNext_Level());
        }

        if (TokensCollected == Tokens_ToWin)
        {
            DeadEnemies = 0;
        }
    }

    IEnumerator DelayNext_Level()
    {
        yield return new WaitForSeconds(3);
        if (NextLevel_Cam == null)
        {
            print("null on level");
        }
        else
        {
            NextLevel_Cam.gameObject.SetActive(true);
            CurrentLevel_Cam.gameObject.SetActive(false);
        }
        playerJumps_Available = 15;
        Jumps = 15;
    }

    public void Re_Play()
    {
        NextLevel_Cam.gameObject.SetActive(true);
        CurrentLevel_Cam.gameObject.SetActive(false);
        SceneManager.GetActiveScene();
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void GroundChecker()
    {
        if (isGrounded == true)
        {
            Jumps = JumpsValue;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void DoubleJump()
    {
        if (playerJumps_Available >= 0) 
        {
            if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0 && CanJump == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                Jumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Jumps == 0 && isGrounded == true && CanJump == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }

    private void Damage()
    {
        if (playerHealth == 6)
        {
            Destroy(Health_1);
        }
        if (playerHealth == 4)
        {
            Destroy(Health_2);
        }
        if (playerHealth == 2)
        {
            Destroy(Health_3);
            Platforms.SetActive(false);
            Buttons.SetActive(false);
            PlayAgain.SetActive(true);
        }
        if (playerHealth == 0)
        {
            
        }
    }

    private void Movement()
    {
        Vector3 CharacterScale = transform.localScale;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * runSpeed * Time.deltaTime;
            Left.Select();
            Left.OnSelect(null);
            CharacterScale.x = -1.8f;
            anim.SetBool("IsWalkin", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("IsWalkin", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * runSpeed * Time.deltaTime;
            Right.Select();
            Right.OnSelect(null);
            CharacterScale.x = 1.8f;
            anim.SetBool("IsWalkin", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("IsWalkin", false);
        }

        transform.localScale = CharacterScale;

        if (Input.GetKeyDown(KeyCode.Space) && CanJump == true)
        {
            Space.Select();
            Space.OnSelect(null);
            playerJumps_Available = playerJumps_Available - 1;
            anim.SetBool("IsJumpin", true);
            anim.SetBool("IsWalkin", false);
            coinsCollected.DisableAvailable_Jumps(playerJumps_Available);
            if (playerJumps_Available <= 0)
            {
                NoIcon_Activate();
                Store_Question.gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("IsJumpin", false);
        }
    }

    public void NoIcon_Activate()
    {
        Player_NoIcon.GetComponent<Image>().enabled = true;
        Enemy_NoIcon.GetComponent<Image>().enabled = false;
    } //when it is player turn darken Enemy Icon

    public void NoIcon_Deactivate()
    {
        Player_NoIcon.GetComponent<Image>().enabled = false;
        Enemy_NoIcon.GetComponent<Image>().enabled = true;
    } //when it is enemy turn darken Player Icon

    public void YesBuy()
    {
        Store_Question.gameObject.SetActive(false);
        coinsCollected.Store.gameObject.SetActive(true);
        playerJumps_Available++;
    } 
    public void NoBuy()
    {
        Store_Question.gameObject.SetActive(false);
        playerJumps_Available = 3;
        foreach (GameObject Enemy in Enemies)
        {
            CanJump = false;
            if (Enemy.GetComponent<EnemyController>() == null)
            {
                print("nullified");
            }
            else
            {
                Enemy.GetComponent<EnemyController>().enabled = true;
                Enemy.GetComponent<Animator>().SetBool("IsWalkin", true);
            }

            if (Enemy.gameObject.name == "ShootinEnemy")
            {
                Enemy.GetComponentInChildren<ShootProjectile>().enabled = true;
            }

            if (Enemy.gameObject.name == "Boss")
            {
                Enemy.GetComponentInChildren<ShootProjectile>().enabled = true;
            }

            if (Enemy.gameObject.name == "WalkinEnemy")
            {
                Enemy.GetComponent<Animator>().SetBool("IsWalkin", true);
            }

        }
    }
 
    void EnemyDeathEffect()
    {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        Instantiate(EnemyDeath_Effect, transform.position, transform.rotation);
    }

    public void Play_Again()
    {
        Platforms.SetActive(true);
        Buttons.SetActive(true);
        PlayAgain.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void AddJump(int originalPlayerJumps)
    {
        playerJumps_Available = playerJumps_Available + 1;
    }

    IEnumerator ResetJumpsTo_Die()
    {
        yield return new WaitForSeconds(ResetJumpsToKillEnemy_Duration);
        No_Of_JumpsToKill_Enemies = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))    //if player collides with enemy on their head(1.0f contact point) then kill enemy, otherwise hurt playerand remove one heart
        {                                                //when player runs out of herats start the No_Heart coroutine
            foreach (ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal); //kill enemy
                if (point.normal.y >= 0.9f)
                {
                    rb.velocity = Vector2.up * EnemyKillJump;
                    //AddJump();
                    if(collision.gameObject.name == "WalkinEnemy")
                    {
                        EnemyDeathEffect();
                        No_Of_JumpsToKill_Enemies = No_Of_JumpsToKill_Enemies + 1;
                        if(No_Of_JumpsToKill_Enemies == 2)
                        {
                            DeadEnemies = DeadEnemies + 1;
                            collision.gameObject.SetActive(false);
                            Instantiate(EnemyToken, collision.gameObject.transform.position, Quaternion.identity);
                        }
                        StartCoroutine(ResetJumpsTo_Die());
                    }

                    if (collision.gameObject.name == "ShootinEnemy")
                    {
                        EnemyDeathEffect();
                        No_Of_JumpsToKill_Enemies = No_Of_JumpsToKill_Enemies + 1;
                        if (No_Of_JumpsToKill_Enemies == 4)
                        {
                            DeadEnemies = DeadEnemies + 1;
                            collision.gameObject.SetActive(false);
                            Instantiate(EnemyToken, collision.gameObject.transform.position, Quaternion.identity);
                        }
                        StartCoroutine(ResetJumpsTo_Die());
                    }

                    if (collision.gameObject.name == "AI_WalkinEnemy")
                    {
                        EnemyDeathEffect();
                        No_Of_JumpsToKill_Enemies = No_Of_JumpsToKill_Enemies + 1;
                        if (No_Of_JumpsToKill_Enemies == 4)
                        {
                            DeadEnemies = DeadEnemies + 1;
                            collision.gameObject.SetActive(false);
                            Instantiate(EnemyToken, collision.gameObject.transform.position, Quaternion.identity);
                        }
                        StartCoroutine(ResetJumpsTo_Die());
                    }

                    if (collision.gameObject.name == "Boss")
                    {
                        EnemyDeathEffect();
                        No_Of_JumpsToKill_Enemies = No_Of_JumpsToKill_Enemies + 1;
                        if (No_Of_JumpsToKill_Enemies == 12)
                        {
                            DeadEnemies = DeadEnemies + 1;
                            collision.gameObject.SetActive(false);
                            Instantiate(EnemyToken, collision.gameObject.transform.position, Quaternion.identity);
                        }
                        StartCoroutine(ResetJumpsTo_Die());
                    }
                }

                if (point.normal.x >= 0.8f) //damaj player
                {
                    playerHealth = playerHealth - 1;
                    shake.CamShake();
                    AudioSource.PlayClipAtPoint(PlayerDamage, transform.position);
                    StartCoroutine(Play_InjuryAnim());
                }
                else if (point.normal.x <= -0.8f)
                {
                    playerHealth = playerHealth - 1;
                    shake.CamShake();
                    AudioSource.PlayClipAtPoint(PlayerDamage, transform.position);
                    StartCoroutine(Play_InjuryAnim());
                }
            }
        }
    }

    IEnumerator Play_InjuryAnim()
    {
        anim.SetBool("Ishurt", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Ishurt", false);
    }
}
