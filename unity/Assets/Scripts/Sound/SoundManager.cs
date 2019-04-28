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
       
        m_gameStateParameter = FmodEvent.GetParameterId(musicEvent, "GameState");
    }

    private void Start()
    {
        RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 0);
        m_musicInstance.start();
    }

    private void Update()
    {

        if (GameManager.Instance.currentGamestate == GameManager.GameState.PLAYING)
        {
            RuntimeManager.StudioSystem.setParameterByID(m_gameStateParameter, 1);
        }

        Debug.Log(GameManager.Instance.currentGamestate);
        /*switch (GameManager.Instance.currentGamestate)
        {
            case GameManager.GameState.MAINMENU:
                m_musicInstance.setParameterByID(m_gameStateParameter,0);
                break;
            case GameManager.GameState.PLAYING:
                m_musicInstance.setParameterByID(m_gameStateParameter, 1);
                break;
            case GameManager.GameState.WINSCREEN:
                m_musicInstance.setParameterByID(m_gameStateParameter, 5);
                break;
            default:
                m_musicInstance.setParameterByID(m_gameStateParameter,0);
                break; 
        }*/
    }
}
