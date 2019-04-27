using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;


    public GameObject playerOne;
    public GameObject playerTwo; 

    public int playerOneDeaths;
    public int playerTwoDeath; 
   
    public event Action gameOverEvent;

    private GameObject levelScript;
    
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

       levelScript = FindObjectOfType<LevelScript>();

    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            GameOver();
            
        }
    }

    public void StartGame()
    {
        currentGamestate = GameState.PLAYING;
        
        LightUpTile();

        
    }



    public void GameOver()
    {
        currentGamestate = GameState.WINSCREEN; 
        if (gameOverEvent != null)
        {
            gameOverEvent();
        }
        
    }

    public void SteppedOnWalkableTile(Collider tile, PlayerMovement actor)
    {
        ;
    }

    public void SteppedOnNonWalkableTile(Collider tile, PlayerMovement actor)
    {
        actor.Die();
       
    }

    public void SteppedOnFinishTile(Collider tile, PlayerMovement actor)
    {
        actor.Win();
        GameOver();
    }

    public void RegisterPlayer(GameObject playerOne, GameObject playerTwo)
    {
        this.playerOne = playerOne;
        this.playerTwo = playerTwo;
        

    }


    public void RegisterTiles(GameObject _tiles)
    {
        this.levelScript = _tiles;
    }


    public void MainMenu()
    {

        SceneManager.LoadSceneAsync("Main Menu");

    }


    public void LightUpTile()
    {
        foreach (var tile in levelScript.walkableTiles)       
        {                                                     
                                                      
            Renderer renderer = GetComponent<Renderer>();     
                                                      
            renderer.material.EnableKeyword("_EMISSION");     
                                                      
        }                                                     
        
    }


   
   
   
   
   
   






}

    