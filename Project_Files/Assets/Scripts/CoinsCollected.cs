using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinsCollected : MonoBehaviour
{
    [SerializeField] private GameObject[] Coin;
    [SerializeField] private GameObject[] Jumps;
    public GameObject Store;
    bool startTimer;
    [SerializeField] PlayerController playerController;
    public bool Bought;
    [SerializeField] private float timeRemaining;
    public int No_CoinsCollected;
    public int No_CoinsSpent;
    [SerializeField] private Text coinText;
    [SerializeField] private Button BuyButton_TwoCoins;
    [SerializeField] private Button BuyButton_Infinite;

    // Start is called before the first frame update
    void Start()
    {
        Bought = false;
        Coin = GameObject.FindGameObjectsWithTag("Coin");
        Jumps = GameObject.FindGameObjectsWithTag("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "You have: " + No_CoinsCollected.ToString() + "$";
        CollectCoin();

        if (startTimer)
        {
            if (timeRemaining > 0)
            {
                playerController.playerJumps_Available = 30;
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                startTimer = false;
                playerController.playerJumps_Available = 3;
            }
        }

        if (No_CoinsCollected >= 3)
        {
            BuyButton_TwoCoins.interactable = true;
            BuyButton_Infinite.interactable = true;
        }
        else if (No_CoinsCollected < 3)
        {
            BuyButton_Infinite.interactable = false;
        }

        if(No_CoinsCollected >= 2)
        {
            BuyButton_TwoCoins.interactable = true;
        }
        else if (No_CoinsCollected < 2)
        {
            BuyButton_TwoCoins.interactable = false;
        }
    }

    public void BuyJump_x1()
    {
        No_CoinsCollected = No_CoinsCollected - 2;
        No_CoinsSpent = No_CoinsSpent + 2;
        Bought = true;

        foreach(GameObject Jump in Jumps)
        {
            if(Jump.gameObject.name == "Jump4")
            {
                Jump.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void BuyJump_Infinite()
    {
        No_CoinsCollected = 0;
        No_CoinsSpent = No_CoinsSpent + 3;
        startTimer = true;
       
        foreach (GameObject Jump in Jumps)
        {
            if (Jump.gameObject.name == "Jump_Infinity")
            {
                Jump.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void ExitShop()
    {
        Store.gameObject.SetActive(false);
        DisableAvailable_Jumps(playerController.playerJumps_Available);

        if (Bought)
        {
            playerController.AddJump(playerController.playerJumps_Available = 3);
            Bought = false;
        }
        else if (!Bought)
        {
            playerController.playerJumps_Available = 3;
        }

        foreach (GameObject Enemy in playerController.Enemies)
        {
            playerController.CanJump = false;
            if (Enemy.GetComponent<EnemyController>() == null)
            {
                print("nullified");
            }
            else
            {
                Enemy.GetComponent<EnemyController>().enabled = true;
                Enemy.GetComponent<Animator>().SetBool("IsWalkin", true);
            }

            if (Enemy.gameObject.name == "Boss")
            {
                Enemy.GetComponentInChildren<ShootProjectile>().enabled = true;
            }

            if (Enemy.gameObject.name == "ShootinEnemy")
            {
                Enemy.GetComponentInChildren<ShootProjectile>().enabled = true;
            }

            if (Enemy.gameObject.name == "WalkinEnemy")
            {
                Enemy.GetComponent<Animator>().SetBool("IsWalkin", true);
            }
        }
    }

    public void DisableAvailable_Jumps(int playerJumps_Available)
    {
        foreach(GameObject Jump in Jumps)
        {
            if (Jump.gameObject.name == "Jump4" && playerJumps_Available == 3)
            {
                Jump.GetComponent<SpriteRenderer>().enabled = false;
            }

            else if(Jump.gameObject.name == "Jump1" && playerJumps_Available == 2)
            {
                Jump.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(Jump.gameObject.name == "Jump2" && playerJumps_Available == 1)
            {
                Jump.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (Jump.gameObject.name == "Jump3" && playerJumps_Available == 0)
            {
                Jump.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void EnableAvailable_Jumps()
    {
          foreach(GameObject Jump in Jumps)
        {
            if (Jump.gameObject.name == "Jump1" || Jump.gameObject.name == "Jump2" || Jump.gameObject.name == "Jump3")
            {
                Jump.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    private void CollectCoin()
    {
        foreach(GameObject Coin in Coin)
        {
            if (Coin.gameObject.name == "Coin1" && No_CoinsCollected == 1)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
                print("Coin1");
            }
            else if (Coin.gameObject.name == ("Coin2") && No_CoinsCollected == 2)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin3") && No_CoinsCollected == 3)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin3") && No_CoinsCollected == 3)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin4") && No_CoinsCollected == 4)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin5") && No_CoinsCollected == 5)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin6") && No_CoinsCollected == 6)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin7") && No_CoinsCollected == 7)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(Coin.gameObject.name == ("Coin8") && No_CoinsCollected == 8)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        foreach(GameObject Coin in Coin)
        {
            if(Coin.gameObject.name == ("Coin1") && No_CoinsSpent == 2)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = false;
            }
            if(Coin.gameObject.name == ("Coin2") && No_CoinsSpent == 2)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = false;
            }

            if(Coin.CompareTag("Coin") && No_CoinsSpent == 8)
            {
                Coin.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            No_CoinsCollected++;
            print(No_CoinsCollected);
        }
    }
}