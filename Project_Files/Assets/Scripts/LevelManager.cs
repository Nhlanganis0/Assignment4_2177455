using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{
    PlayerTurn,
    EnemyTurn
}
public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] EnemyController enemyController;

    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.PlayerTurn)
        {
            playerController.Update();
        }
        else if(state == GameState.EnemyTurn)
        {
            enemyController.Update();
        }
    }
}
