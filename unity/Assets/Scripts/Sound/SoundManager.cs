using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [EventRef]
    public string buttonHover;
    
    [EventRef]
    public string buttonClick;

    [EventRef]
    public string musicEvent;

    private PARAMETER_ID m_gameStateParameter;
    private EventInstance m_musicInstance; 


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        m_musicInstance = RuntimeManager.CreateInstance(musicEvent);
       
        m_gameStateParameter = FmodEvent.GetParameterId(musicEvent, "MusicStates");
    }

    private void Start()
    {
        RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 0);
        m_musicInstance.start();
    }

    private void Update()
    {


        
        switch (GameManager.Instance.currentGamestate)
        {
            case GameManager.GameState.MAINMENU:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter,0);
                break;
            case GameManager.GameState.PLAYING:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 1);
                break;
            
            case GameManager.GameState.PLAYING2:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 2);
                break;
                
            case GameManager.GameState.PLAYING3:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 3);
                break;
            
            case GameManager.GameState.PLAYING4:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 4);
                break;

            case GameManager.GameState.WINSCREEN:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 5);
                break;
            default:
                RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter,0);
                break; 
        }
    }
}
