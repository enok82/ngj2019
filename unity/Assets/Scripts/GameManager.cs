using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;


    public GameObject playerOne;
    public GameObject playerTwo; 

    public int playerOneDeaths;
    public int playerTwoDeath; 
    
    public enum GameState
    {
        MAINMENU, 
        PLAYING,
        WINSCREEN,
    }
    public  GameState currentGamestate;

    public bool gameIsStarted = false; 
    
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
        playerOneDeaths = 0;
        playerTwoDeath = 0;

        currentGamestate = GameState.MAINMENU;

    }

    public void StartGame()
    {
        currentGamestate = GameState.PLAYING; 
    }



    public void GameOver()
    {
        currentGamestate = GameState.WINSCREEN; 
    }

    public void SteppedOnWalkableTile(Collider tile, PlayerMovement actor)
    {
        ;
    }

    public void SteppedOnNonWalkableTile(Collider tile, PlayerMovement actor)
    {
        actor.Die();
       
    }


    public void RegisterPlayer(GameObject playerOne, GameObject playerTwo)
    {
        playerOne = this.playerOne;
        playerTwo = this.playerTwo; 

    }
    
    

    
    
    
    
}
