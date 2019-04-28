using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System;
using FMODUnity;
using HutongGames.PlayMaker.Actions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;


    public GameObject playerOne;
    public GameObject playerTwo; 

    public int playerOneDeaths;
    public int playerTwoDeath;

    public string playerOneControllerScheme;
    public string playerTwoControllerScheme;

    public event Action gameOverEvent;
    public event Action startGameEvent;

    public float waitTime;
    
    [HideInInspector]
    public float countDownTime;


    private LevelScript m_levelScript;
    private PlayerMovement playerOneControls;
    private PlayerMovement playerTwoControls;

    
    [EventRef]
    public string showtiles;
    
    [EventRef]
    public string hidetiles;
    
    [EventRef]
    public string wonGame;
    
    [EventRef]
    public string unlockTiles;
    
    [EventRef]
    public string playerDeath;
    

    public int walkableTilesCount;
    
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

        countDownTime = waitTime;

        //m_levelScript = FindObjectOfType<LevelScript>();

    }


    private void Start()
    {

        
        

    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            GameOver();
            
        }
        
        
        //Debug.Log(currentGamestate);
    }

    public void StartGame()
    {
        currentGamestate = GameState.PLAYING;


        if (startGameEvent != null)
        {
            startGameEvent();
        }
        
        LightUpTiles();
        walkableTilesCount = m_levelScript.walkableTiles.Count;


    }



    public void GameOver()
    {
        currentGamestate = GameState.WINSCREEN; 
        if (gameOverEvent != null)
        {
            gameOverEvent();
        }

        FmodEvent.PlayOneShot(wonGame, transform, null);

    }

    public void SteppedOnWalkableTile(Collider tile, PlayerMovement actor)
    {
        Renderer renderer = tile.GetComponent<Renderer>();
        renderer.material.EnableKeyword("_EMISSION");

        FmodEvent.PlayOneShot(unlockTiles, transform, null);
    }

    public void SteppedOnNonWalkableTile(Collider tile, PlayerMovement actor)
    {
        actor.Die();
        FmodEvent.PlayOneShot(playerDeath, transform, null);


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
        
        playerOneControls = playerOne.GetComponent<PlayerMovement>();                               
        playerTwoControls = playerTwo.GetComponent<PlayerMovement>();                               
        

    }


    public void RegisterLevelScript(LevelScript _levelScript)
    {
        this.m_levelScript = _levelScript;
    }


    public void MainMenu()
    {

        SceneManager.LoadSceneAsync("Main Menu");
        currentGamestate = GameState.MAINMENU;

    }
    
    
    public void LightUpTiles()                            
    {                                                     
        
          
        
        StartCoroutine(LightUpSequence(waitTime));        
                                                          
    }                                                     
                                                          
                                                          
    public IEnumerator LightUpSequence(float waitTime)
    {
        if (playerOneControls != null && playerTwoControls != null)
        {
            playerOneControls.enabled = false;
            playerTwoControls.enabled = false;  
        }
    
        
        EnableEmission();                                             
                                                          
        yield return new WaitForSeconds(waitTime);        
		                                                  
        DisableEmission();
        
     
       
       if (playerOneControls != null && playerTwoControls != null)
       {
           playerOneControls.enabled = true; 
           playerTwoControls.enabled = true; 
       }
        
       
    }


    public void EnableEmission()
    {
        
        foreach (var tile in m_levelScript.walkableTiles)              
        {                                                 
            Renderer renderer = tile.GetComponent<Renderer>();
            
            renderer.material.EnableKeyword("_EMISSION");
            
            
            
            
            Debug.Log("Lighted Up");	                      
        }

        FmodEvent.PlayOneShot(showtiles, transform, null);

    }
    
    public void DisableEmission()
    {
        
        foreach (var tile in m_levelScript.walkableTiles)              
        {                                                 
            Renderer renderer = tile.GetComponent<Renderer>();
            
            renderer.material.DisableKeyword("_EMISSION");
            
            
            Debug.Log("Lighted Down");	                      
        } 
        
        FmodEvent.PlayOneShot(hidetiles, transform, null);

        
    }

    public void SetPlayerOneControllerScheme()
    {
        if (playerOneControllerScheme == "A")
        {
            playerOneControllerScheme = "B";
        }
        else
        {
            playerOneControllerScheme = "A";
        }
    }

    public void SetPlayerTwoControllerScheme()
    {
        if (playerTwoControllerScheme == "A")
        {
            playerTwoControllerScheme = "B";
        }
        else
        {
            playerTwoControllerScheme = "A";
        }
    }
}


   
   
   
   
   
   








    