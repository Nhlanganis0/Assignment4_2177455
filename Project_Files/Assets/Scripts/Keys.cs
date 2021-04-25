using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    private CoinsCollected coinsCollected;
    [SerializeField] GameObject CoinsCollected_Object;

    private void Start()
    {
        CoinsCollected_Object = GameObject.Find("CoinsCollected");
        coinsCollected = CoinsCollected_Object.GetComponent<CoinsCollected>();
    }
    private void OnTriggerEnter2D(Collider2D collision) //for tokens,
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinsCollected.No_CoinsCollected++;
            Destroy(gameObject, 0.2f);
        }
    }
}
